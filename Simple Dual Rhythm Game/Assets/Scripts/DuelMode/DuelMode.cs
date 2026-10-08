using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelMode : GameLoop
{
    [SerializeField] private DuelManager _gameManager;
    [SerializeField] private PlayersManager playersScript;
    [SerializeField] private Metronome _metronome;
    [SerializeField] private PartUI UIScript;
    [SerializeField] private Parameters _parameters;
    
    GameState currentState = GameState.Playing;
    GameState nextState = GameState.Playing;

    public GameState[] statesSeries;
    int currentStateIndex;
    private int nextStateIndex;
    
    private bool _isGameEnded;
    private bool _isLastSolo;
    private bool _isPausingForEmptySolo;
    
    private int riffLength;
    private int notesSucceeded;
    private List<NoteIcon> _currentTrackedNotes = new List<NoteIcon>();
    
    bool madeMistake = false;
    
    public enum GameState
    {
        Recording,
        Playing,
        Silence,
        ChangePlayer
    };
    
    private void Awake() 
    {
        currentStateIndex = statesSeries.Length - 1;
        nextStateIndex = 0;
        UIScript.SetUp(_parameters.bpm, _parameters.beatPerBar, AddTrackedNote, RemoveTrackedIcon);
    }

    private void Start()
    {
        _metronome.OnFirstTick += FirstTick;
        _metronome.OnNewBar += NewBar;
        _metronome.OnNewCycle += NextPhase;
        _metronome.OnTick += Tick;
        _metronome.OnTempoChanged += UIScript.ChangeTempo;
        
        //Add through SO?
        _gameManager.stopGame.AddListener(EndGame);
    }

    public override void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex) 
    {
        if (playerIndex != currentPlayerIndex) return;

        //Maybe modify with timing, or collision or something
        if (currentState == GameState.Silence && nextState == GameState.Recording) return;
        
        if (currentState == GameState.Recording) {
            riffLength++;
            UIScript.DrawNewNote(noteIndex);

            PlayNoteSound(noteIndex);
        }
        else if (currentState == GameState.Playing || nextState == GameState.Playing) {
            if (IsRightNote(noteIndex)) {
                notesSucceeded++;
                int points = notesSucceeded * 10;
                playersScript.MakePoints(points);

                if (currentState == GameState.Silence)
                {
                    Debug.Log("Made points in silence");
                }

                //Bonus points for a perfect solo
                if (notesSucceeded == riffLength && !madeMistake) {
                    //Should be 200 for composer and 400 for the other, will change at some point
                    playersScript.MakePoints(400);
                }

                PlayNoteSound(noteIndex);
            }
            else {
                madeMistake = true;
                //int penalty = ((riffLength * (riffLength + 1)) / 2 * 10) / riffLength;

                int penalty = 0;
                switch (riffLength)
                {
                    case < 4: penalty = 10;
                        break;
                    case < 10: penalty = 20;
                        break;
                    case < 25: penalty = 50;
                        break;
                    default: penalty = 100;
                        break;
                }
                
                playersScript.MakePoints(-penalty);
                _audioManager.PlaySound("WrongNote");
            }
        }
    }
    
    bool IsRightNote(int noteIndex) 
    {
        float smallestSqrDistance = 1000f;
        NoteIcon closestIcon = null;

        foreach(NoteIcon note in _currentTrackedNotes)
        {
            if (note.GetState() == NoteState.Unplayed) 
            {
                if (note.GetIndex() == noteIndex)
                {
                    note.ChangeState(NoteState.Played);
                    _currentTrackedNotes.Remove(note);
                    return true;
                }
                else if (Vector3.SqrMagnitude(note.transform.position - UIScript.currentTracker.transform.position) < smallestSqrDistance)
                { 
                    closestIcon = note;
                }
            }
        }

        if (closestIcon != null)
        {
            closestIcon.ChangeState(NoteState.Wrong);
            _currentTrackedNotes.Remove(closestIcon);
        }

        return false;
    }
    
    public void AddTrackedNote(NoteIcon noteIcon) 
    {
        if (currentState == GameState.Playing || (currentState == GameState.Silence && nextState == GameState.Playing)) 
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
            }

            _currentTrackedNotes.Remove(noteIcon);
        }
    }
    
    private void PlayNoteSound(int noteIndex) {
        if (!_isLastSolo)
        {
            _audioManager.PlayNote(playersScript.CurrentPlayer.index, noteIndex);
        }
        else
        {
            _audioManager.PlayFinaleNote(playersScript.CurrentPlayer.index, noteIndex);
        }
    }

    void FirstTick()
    {
        _audioManager.PlayBeat();
    }

    void Tick(int beat)
    {
        if (currentState == GameState.Silence)
        {
            UIScript.UpdateCountdown(beat);
        }
    }

    void NewBar()
    {
        if (currentState == GameState.Silence)
        {
            ChangeState(currentStateIndex + 1);
            ChangeNextState(nextStateIndex + 1);
            
            bool playAgain = GetPreviousState(currentStateIndex) == GameState.ChangePlayer;
            
            UIScript.ClearCountdown();
            UIScript.ChangeNextStateMessage(nextState, playAgain);
        }
    }

    void NextPhase()
    {
        if (currentState == GameState.Recording && riffLength == 0)
        {
            EmptyRiffAlert();
            
            //Temporary workaround
            _metronome.IncreaseMetronomeCounter();
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
        if (nextState == GameState.Recording) {
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
    
    void ChangeState(int newStateIndex)
    {
        currentStateIndex = newStateIndex;

        if (currentStateIndex == statesSeries.Length)
        {
            currentStateIndex = 0;
            UIScript.EraseAllNotes();
            _gameManager.AddSolo();
        }

        currentState = statesSeries[currentStateIndex];
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
        }
    }
    
    GameState GetPreviousState(int newStateIndex)
    {
        int previousStateIndex = newStateIndex;
        previousStateIndex--;

        if (previousStateIndex < 0)
        {
            previousStateIndex = statesSeries.Length - 1;
        }
        
        return  statesSeries[previousStateIndex];
    }
    
    private void SetNextBar()
    {
        UIScript.NewBar(nextState);

        bool playAgain = GetPreviousState(currentStateIndex) != GameState.ChangePlayer && nextState == GameState.Playing;
        
        UIScript.ChangeNextStateMessage(nextState, playAgain);
    }
    
    public void SetLastSolo()
    {
        _isLastSolo = true;
    }
    
    private void EmptyRiffAlert() 
    {
        _isPausingForEmptySolo = true;
        _pauseMenu.TogglePauseMenuBehaviour();
        UIScript.SetForgotRecordUI(true);

        Time.timeScale = 0;
    }
    
    //Double-check that this doesn't interact with regular pause
    public void OnRPressed(int playerIndex, int currentPlayerIndex) 
    {
        if(_isPausingForEmptySolo && (playerIndex == -1 || playerIndex == currentPlayerIndex))
        {
            Time.timeScale = 1;

            ChangeState(0);
            ChangeNextState(1);
            SetNextBar();

            _pauseMenu.TogglePauseMenuBehaviour();
            _isPausingForEmptySolo = false;
            UIScript.SetForgotRecordUI(false);

            //??
            UIScript.currentTracker = null;
        }
    }
    
    public void EndGame() 
    {
        _isGameEnded = true;
        enabled = false;
    }
}
