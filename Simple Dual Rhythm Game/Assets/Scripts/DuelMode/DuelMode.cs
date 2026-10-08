using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelMode : GameLoop
{
    [SerializeField] private Parameters _parameters;
    [SerializeField] private PartUI UIScript;
    [SerializeField] private Metronome _metronome;
    [SerializeField] private PlayersManager playersScript;
    
    public GameState[] statesSeries;
    public GameState currentState = GameState.Playing;
    public GameState nextState = GameState.Playing;
    public int currentStateIndex;
    public int nextStateIndex;
    
    private int _riffLength;
    private int notesSucceeded;
    bool madeMistake = false;
    
    private List<NoteIcon> _currentTrackedNotes = new List<NoteIcon>();

    private void Awake()
    {
        currentStateIndex = statesSeries.Length - 1;
        nextStateIndex = 0;
        
        _metronome.OnNextPhase += NextPhase;
        _metronome.OnTick += Tick;
    }

    private void Start()
    {
        UIScript.SetUp(_parameters.bpm, _parameters.beatPerBar, AddTrackedNote, RemoveTrackedIcon);
    }

    public override void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex) 
    {
        if (playerIndex != currentPlayerIndex) return;

        //Maybe modify with timing, or collision or something
        if (currentState == GameState.Silence && nextState == GameState.Recording) return;
        
        if (currentState == GameState.Recording) {
            _riffLength++;
            UIScript.DrawNewNote(noteIndex);

            PlayNoteSound(noteIndex);
        }
        /*else if (currentState == GameState.Playing || nextState == GameState.Playing) {
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
        }*/
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
        if (currentState == GameState.Silence)
        {
            UIScript.UpdateCountdown(beat);
        }
    }
    
    void NextPhase()
    {
        Debug.Log("Next Phase");
        
        /*if (currentState == GameState.Recording && riffLength == 0)
        {
            EmptyRiffAlert();
            metronomeCounter++;
            return;
        }*/

        _metronome.ChangeState(currentStateIndex + 1);
        _metronome.ChangeNextState(nextStateIndex + 1);
        //Reset the riff if we're recording again, change player if it's a silence...
        //TODO: How to take the error margin into account?
        if (currentState == GameState.ChangePlayer) {
            playersScript.changeCurrentPlayer();
            _metronome.ChangeState(currentStateIndex + 1);
        }
        if (nextState == GameState.Recording) {
            _riffLength = 0;
        }
        else if (currentState == GameState.Playing) {
            //Actual comment
            //riffCounter = 0;
        }
        else {
            UIScript.UnPlayedAllNotes();
        }

        /*if (_isGameEnded) 
        {
            return;
        }*/

        SetNextBar();

        //Change this code slightly when supporting multiple bars
        notesSucceeded = 0;
        madeMistake = false;
    }
    
    private void SetNextBar()
    {
        UIScript.NewBar(nextState);

        /*bool playAgain = GetPreviousState(currentStateIndex) != GameState.ChangePlayer && nextState == GameState.Playing;

        UIScript.ChangeNextStateMessage(nextState, playAgain);*/
    }

    private void PlayNoteSound(int noteIndex) {
        /*if (!_isLastSolo)
        {
            _audioManager.PlayNote(playersScript.CurrentPlayer.index, noteIndex);
        }
        else
        {
            _audioManager.PlayFinaleNote(playersScript.CurrentPlayer.index, noteIndex);
        }*/
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
}

public enum GameState
{
    Recording,
    Playing,
    Silence,
    ChangePlayer
};