using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Metronome : GameLoop
{
    [SerializeField] private Parameters _parameters;
    [SerializeField] private GameManager _gameManager;
    
    public int bpm;

    [HideInInspector]
    public float frequency;
    public int beatPerBar;
    public int bars = 1;
    private int metronomeCounter = 0;
    private int mod = 1;

    private float initialTime;
    private float newBarTime;

    private AudioSource tickSound;
    private AudioSource[] instrumentSounds;
    private AudioSource[] sounds;
    private AudioSource badNoteSound;
    [SerializeField] private AudioSource beat;

    [SerializeField] private AudioSource[] _finaleSounds;

    private float errorMargin = 0.2f;
    private int notesSucceeded;

    [Range(0, 1)]
    public float strongTick;
    [Range(0, 1)]
    public float weakTick;

    [SerializeField] private PlayersManager playersScript;
    [SerializeField] private PartUI UIScript;

    //Should be in a UI class
    [SerializeField] private GameObject _nextStateMessagePanel;
    [SerializeField] private TextMeshProUGUI _nextStateMessageText;
    [SerializeField] private GameObject[] _nextPlayerIcon;
    [SerializeField] private GameObject _recordIcon;
    [SerializeField] private GameObject _playIcon;
    [SerializeField] private GameObject _forgotRecordUI;

    private List<NoteIcon> _currentTrackedNotes = new List<NoteIcon>();

    [SerializeField] private PauseMenu _pauseMenu;

    private int riffLength;

    bool firstTick = true;
    bool madeMistake = false;

    private bool _isLastSolo;

    private bool _isPausingForEmptySolo;
    private bool _isGameEnded;
    
    public enum GameState {
        Recording,
        Playing,
        Silence,
        ChangePlayer
    };

    GameState currentState = GameState.Playing;
    GameState nextState = GameState.Playing;

    public GameState[] statesSeries;
    int currentStateIndex;
    private int nextStateIndex;

    private void Awake() 
    {
        sounds = GetComponents<AudioSource>();
        tickSound = sounds[0];

        instrumentSounds = new AudioSource[4];

        for (int i = 0; i < 4; i++) {
            instrumentSounds[i] = sounds[i + 1];
        }

        currentStateIndex = statesSeries.Length - 1;
        nextStateIndex = 0;

        badNoteSound = sounds[5];

        enabled = false;
    }

    public void StartGame()
    {
        bpm = _parameters.bpm;
        beatPerBar = _parameters.beatPerBar;
        bars = _parameters.bars;

        SetUp();
        SetBeatSpeed();
        enabled = true;

        //Cleaner: register itself through a SO?
        _pauseMenu._onGamePausedOrUnpaused.AddListener(OnGamePaused);
        _gameManager.stopGame.AddListener(EndGame);
    }

    void SetBeatSpeed() {
        AudioMixerGroup pitchBendGroup = Resources.Load<AudioMixerGroup>("MyAudioMixer");
        beat.outputAudioMixerGroup = pitchBendGroup;
        float speed = bpm / 60f;
        beat.pitch = speed;
        pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / speed);
    }

    public void SetUp() {
        frequency = 60f / bpm;
        UIScript.SetUp(this.bpm, this.beatPerBar, AddTrackedNote, RemoveTrackedIcon);
        initialTime = Time.time;
        metronomeCounter = 0;
    }

    public void ChangeTempo() {
        frequency = 60f / bpm;
        
        //Ça
        metronomeCounter = 0;
        mod = 0;
        initialTime = Time.time;

        UIScript.ChangeTempo(bpm, beatPerBar);
        SetBeatSpeed();
    }

    public void SetLastSolo() {
        _isLastSolo = true;
    }

    public void EndGame() 
    {
        _isGameEnded = true;
        enabled = false;
    }

    void tick() {
        if (firstTick) {
            firstTick = false;
            beat.Play();
        }

        //Check if we're at the beginning of a new bar
        if (metronomeCounter % beatPerBar == 0) {
            tickSound.volume = strongTick;
        }

        //Check if we're at the beginning of a new cycle
        if (metronomeCounter % (beatPerBar * bars) == 0) {
            if (currentState == GameState.Recording && riffLength == 0)
            {
                EmptyRiffAlert();
                metronomeCounter++;
                return;
            }

            ChangeState(currentStateIndex + 1);
            ChangeNextState(nextStateIndex + 1);
            //Reset the riff if we're recording again, change player if it's a silence...
            //TODO: How to take the error margin into account?
            if (currentState == GameState.ChangePlayer) {
                playersScript.changeCurrentPlayer();
                ChangeState(currentStateIndex + 1);
            }
            if (currentState == GameState.Recording) {
                riffLength = 0;
            }
            else if (currentState == GameState.Playing) {
                //riffCounter = 0;
            }
            else {
                UIScript.UnPlayedAllNotes();
            }

            if (_isGameEnded) 
            {
                return;
            }

            SetNextBar();

            //Change this code slightly when supporting multiple bars
            notesSucceeded = 0;
            madeMistake = false;
        }
        else {
            tickSound.volume = weakTick;
        }
        //TODO: Reset counter at 0 instead??
        metronomeCounter++;
        //tickSound.Play();
    }

    void ChangeState(int newStateIndex) {
        currentStateIndex = newStateIndex;

        if (currentStateIndex == statesSeries.Length)
        {
            currentStateIndex = 0;
            UIScript.EraseAllNotes();
            _gameManager.AddSolo();
        }

        currentState = statesSeries[currentStateIndex];
        ChangeVisuals();
    }

    void ChangeNextState(int newStateIndex)
    {
        nextStateIndex = newStateIndex;
        
        if (nextStateIndex == statesSeries.Length)
        {
            nextStateIndex = 0;
        }
        
        nextState = statesSeries[nextStateIndex];
        
         if (nextState == GameState.ChangePlayer)
        {
            nextStateIndex++;
            nextState = statesSeries[nextStateIndex];
            UIScript.TogglePlayer1();
        }
    }

    private void SetNextBar()
    {
        newBarTime = Time.time;
        UIScript.NewBar(nextState);
        ChangeNextStateMessage();
    }

    void Update() {
        float timeSpent = Time.time - initialTime;

        if (timeSpent >= frequency * metronomeCounter + mod) {
            tick();
        }

        WindowsDeviceApiService.Yo();
    }

    public void AddTrackedNote(NoteIcon noteIcon) 
    {
        if (currentState == GameState.Playing) 
        {
            _currentTrackedNotes.Add(noteIcon);
        }
    }

    public void RemoveTrackedIcon(NoteIcon noteIcon) 
    {
        if (currentState == GameState.Playing)
        {
            if (noteIcon.GetState() == NoteState.Unplayed) 
            {
                noteIcon.ChangeState(NoteState.Missed);
                noteIcon.SetMissedIcon();
            }

            _currentTrackedNotes.Remove(noteIcon);
        }
    }

    bool IsRightNote(int noteIndex) 
    {
        foreach(NoteIcon note in _currentTrackedNotes)
        {
            if (note.GetIndex() == noteIndex && note.GetState() == NoteState.Unplayed) 
            {
                note.ChangeState(NoteState.Played);
                note.SetPlayedIcon();
                _currentTrackedNotes.Remove(note);
                return true;
            }
        }

        return false;
    }

    public override void PlayNote(int noteIndex) 
    {
        if (currentState == GameState.Silence) {
            return;
        }
        
        if (currentState == GameState.Recording) {
            riffLength++;
            UIScript.DrawNewNote(noteIndex);

            PlayNoteSound(noteIndex);
        }
        else if (currentState == GameState.Playing) {
            if (IsRightNote(noteIndex)) {
                notesSucceeded++;
                int points = notesSucceeded * 10;
                playersScript.MakePoints(points);

                //Bonus points for a perfect solo
                if (notesSucceeded == riffLength && !madeMistake) {
                    //Should be 200 for composer and 400 for the other, will change at some point
                    playersScript.MakePoints(400);
                }

                PlayNoteSound(noteIndex);
            }
            else {
                madeMistake = true;
                int penalty = ((riffLength * (riffLength + 1)) / 2 * 10) / riffLength;
                playersScript.MakePoints(-penalty);

                //TODO: faire un event? link up le son et le ui ici, avec un paramètre
                //Faire un seul call a l'exterieur

                badNoteSound.Play();
            }
        }
    }

    private void PlayNoteSound(int noteIndex) {
        if (!_isLastSolo)
        {
            instrumentSounds[noteIndex].Play();
        }
        else
        {
            _finaleSounds[noteIndex].Play();
        }
    }

    void ChangeVisuals() {
        //Maybe VFX or whatever
    }

    private void ChangeNextStateMessage()
    {
        foreach(var icon in _nextPlayerIcon) { 
            icon.SetActive(false);
        }

        if (nextState == GameState.Recording)
        {
            _nextStateMessagePanel.SetActive(true);
            _nextStateMessageText.text = "Player " + (playersScript.CurrentPlayer.index + 1) + ": Prepare to record";
            _nextPlayerIcon[playersScript.CurrentPlayer.index].SetActive(true);
            _recordIcon.SetActive(true);
        }
        else if (nextState == GameState.Playing)
        {
            _nextStateMessagePanel.SetActive(true);
            _nextStateMessageText.text = "Player " + (playersScript.CurrentPlayer.index + 1) + ": Prepare to play";
            _nextPlayerIcon[playersScript.CurrentPlayer.index].SetActive(true);
            _playIcon.SetActive(true);
        }
        else if (nextState == GameState.Silence)
        {
            _nextStateMessagePanel.SetActive(false);
            _playIcon.SetActive(false);
            _recordIcon.SetActive(false);
        }
    }

    private void OnGamePaused(bool paused) {
        if (paused)
        {
            beat.Pause();
        }
        else { 
            beat.UnPause();
        }
    }

    private void EmptyRiffAlert() {
        _isPausingForEmptySolo = true;
        OnGamePaused(true);
        _forgotRecordUI.SetActive(true);

        Time.timeScale = 0;
    }

    //Double-check that this doesn't interact with regular pause
    public void OnRPressed() {
        if(_isPausingForEmptySolo)
        {
            Time.timeScale = 1;

            ChangeState(0);
            ChangeNextState(1);
            SetNextBar();

            OnGamePaused(false);
            _isPausingForEmptySolo = false;
            _forgotRecordUI.SetActive(false);

            //??
            UIScript.currentTracker = null;
        }
    }
}