using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelMode : GameLoop
{
    [SerializeField] private Parameters _parameters;
    [SerializeField] private PartUI UIScript;
    [SerializeField] private Metronome _metronome;
    [SerializeField] private DuelManager _gameManager;
    [SerializeField] private PlayersManager playersScript;
    [SerializeField] private List<GameState> statesSeries = new List<GameState>();
    
    private PhasesManager _phasesManager;
    
    private int _riffLength;
    private int notesSucceeded;
    bool madeMistake = false;
    
    private List<NoteIcon> _currentTrackedNotes = new List<NoteIcon>();
    
    private bool _isPausingForEmptySolo;
    private bool _isLastSolo;
    private bool _isGameEnded;

    private void Awake()
    {
        _metronome.OnNextPhase += NextPhase;
        _metronome.OnFirstTick += FirstTick;
        _metronome.OnTick += Tick;
        _metronome.OnNewBar += NewBar;
        _metronome.OnTempoChanged += ChangeTempo;

        _metronome.OnNewBar += UIScript.NewBar;
        
        _phasesManager = _metronome.SpawnPhasesManager(statesSeries);
    }

    private void Start()
    {
        UIScript.SetUp(_parameters.bpm, _parameters.beatPerBar, AddTrackedNote, RemoveTrackedIcon);
        _gameManager.stopGame.AddListener(EndGame);
    }

    public override void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex) 
    {
        if (playerIndex != currentPlayerIndex) return;

        //Maybe modify with timing, or collision or something
        if (PhasesManager.CurrentState == GameState.Silence 
            && PhasesManager.CurrentState == GameState.Recording) return;
        
        if (PhasesManager.CurrentState == GameState.Recording) {
            _riffLength++;
            UIScript.DrawNewNote(noteIndex);

            PlayNoteSound(noteIndex);
        }
        else if (PhasesManager.CurrentState == GameState.Playing 
                 || PhasesManager.NextState == GameState.Playing) {
            if (IsRightNote(noteIndex)) {
                notesSucceeded++;
                int points = notesSucceeded * 10;
                playersScript.MakePoints(points);

                if (PhasesManager.CurrentState == GameState.Silence)
                {
                    Debug.Log("Made points in silence");
                }

                //Bonus points for a perfect solo
                if (notesSucceeded == _riffLength && !madeMistake) {
                    //Should be 200 for composer and 400 for the other, will change at some point
                    playersScript.MakePoints(400);
                }

                PlayNoteSound(noteIndex);
            }
            else {
                madeMistake = true;

                int penalty = 0;
                switch (_riffLength)
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
                AudioManager.Instance.PlaySound("WrongNote");
            }
        }
    }

    public override bool GetEntryGatePosition()
    {
        return PhasesManager.CurrentState == GameState.Silence;
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

    private void Tick(int beat)
    {
        if (PhasesManager.CurrentState == GameState.Silence)
        {
            UIScript.UpdateCountdown(beat);
        }
    }

    private void FirstTick()
    {
        AudioManager.Instance.PlayBeat();
    }

    void NextPhase()
    {
        if (PhasesManager.CurrentState == GameState.Recording && _riffLength == 0)
        {
            EmptyRiffAlert();
            _metronome.IncreaseMetronomeCounter();
            return;
        }

        ChangeState(PhasesManager.CurrentStateIndex + 1);
        ChangeNextState(PhasesManager.NextStateIndex + 1);
        
        //Reset the riff if we're recording again, change player if it's a silence...
        //TODO: How to take the error margin into account?
        if (PhasesManager.CurrentState == GameState.ChangePlayer) {
            playersScript.changeCurrentPlayer();
            ChangeState(PhasesManager.CurrentStateIndex + 1);
        }
        if (PhasesManager.NextState == GameState.Recording) {
            _riffLength = 0;
        }
        else if(PhasesManager.CurrentState != GameState.Playing) {
            UIScript.UnPlayedAllNotes();
        }

        if (_isGameEnded) 
        {
            return;
        }

        //Conditional here? Or called from PhasesManager event???
        SetNextBar();

        //Change this code slightly when supporting multiple bars
        notesSucceeded = 0;
        madeMistake = false;
    }
    
    void ChangeState(int newStateIndex)
    {
        PhasesManager.CurrentStateIndex = newStateIndex;

        if (PhasesManager.CurrentStateIndex == PhasesManager.statesSeries.Count)
        {
            PhasesManager.CurrentStateIndex = 0;
            UIScript.EraseAllNotes();
            _gameManager.AddSolo();
        }

        PhasesManager.CurrentState = PhasesManager.statesSeries[PhasesManager.CurrentStateIndex];
    }
    
    void ChangeNextState(int newStateIndex)
    {
        PhasesManager.NextStateIndex = newStateIndex;
        
        if (PhasesManager.NextStateIndex == PhasesManager.statesSeries.Count)
        {
            PhasesManager.NextStateIndex = 0;
        }
        
        PhasesManager.NextState = PhasesManager.statesSeries[PhasesManager.NextStateIndex];
        
        if (PhasesManager.NextState == GameState.ChangePlayer)
        {
            PhasesManager.NextStateIndex++;
            PhasesManager.NextState = PhasesManager.statesSeries[PhasesManager.NextStateIndex];
        }
    }

    private void NewBar()
    {
        ChangeState(PhasesManager.CurrentStateIndex + 1);
        ChangeNextState(PhasesManager.NextStateIndex + 1);
            
        /*bool playAgain = PhasesManager.GetPreviousState(PhasesManager.CurrentStateIndex) == GameState.ChangePlayer;
            
        UIScript.ClearCountdown();
        UIScript.ChangeNextStateMessage(PhasesManager.NextState, playAgain);*/
    }

    private void SetNextBar()
    {
        bool playAgain = PhasesManager.GetPreviousState(PhasesManager.CurrentStateIndex) != GameState.ChangePlayer && PhasesManager.NextState == GameState.Playing;
        UIScript.SetNextBar(PhasesManager.NextState);
        UIScript.ChangeNextStateMessage(PhasesManager.NextState, playAgain);
    }

    private void PlayNoteSound(int noteIndex) {
        if (!_isLastSolo)
        {
            AudioManager.Instance.PlayNote(playersScript.CurrentPlayer.index, noteIndex);
        }
        else
        {
            AudioManager.Instance.PlayFinaleNote(playersScript.CurrentPlayer.index, noteIndex);
        }
    }
    
    void AddTrackedNote(NoteIcon noteIcon)
    {
        if (PhasesManager.CurrentState == GameState.Playing || (PhasesManager.CurrentState == GameState.Silence && PhasesManager.NextState == GameState.Playing))
        {
            _currentTrackedNotes.Add(noteIcon);
        }
    }

    void RemoveTrackedIcon(NoteIcon noteIcon) 
    {
        if (PhasesManager.CurrentState == GameState.Playing)
        {
            if (noteIcon.GetState() == NoteState.Unplayed)
            {
                noteIcon.ChangeState(NoteState.Missed);
            }

            _currentTrackedNotes.Remove(noteIcon);
        }
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
            UIScript.currentTracker = null;
        }
    }

    public void ChangeTempo(int bpm, int beatPerBar)
    {
        UIScript.ChangeTempo(bpm, beatPerBar);
    }

    public void SetLastSolo()
    {
        _isLastSolo = true;
    }
    
    void EndGame() 
    {
        _isGameEnded = true;
        enabled = false;
        _metronome.EndGame();
    }
}