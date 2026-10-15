using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiffManager : MonoBehaviour
{
    //Access GameLoop here, and DuelMode in DuelRiffManager ish
    
    protected PlayersManager _playersManager;
    //To remove?
    protected PartUI UIScript;
    protected PauseMenu _pauseMenu;
    
    protected int _riffLength;
    protected int notesSucceeded;
    protected bool madeMistake = false;
    
    private bool _isPausingForEmptySolo;
    
    private List<NoteIcon> _currentTrackedNotes = new List<NoteIcon>();

    public Action<int> OnNotePlayed;

    private void Awake()
    {
        OnNotePlayed += PlayNoteSound;
    }

    public void Init(PlayersManager playersManager, PartUI partUI, PauseMenu pauseMenu)
    {
        _playersManager = playersManager;
        UIScript = partUI;
        _pauseMenu = pauseMenu;
    }

    private void OnDestroy()
    {
        OnNotePlayed -= PlayNoteSound;
    }

    public void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex)
    {
        if (playerIndex != currentPlayerIndex) return;

        //Maybe modify with timing, or collision or something
        if (PhasesManager.CurrentState == GameState.Silence 
            && PhasesManager.CurrentState == GameState.Recording) return;
        
        if (PhasesManager.CurrentState == GameState.Recording) {
            _riffLength++;
            //Event for note, with DrawNetNote and PlayNoteSound
            OnNotePlayed?.Invoke(noteIndex);
        }
        else if (PhasesManager.CurrentState == GameState.Playing 
                 || PhasesManager.NextState == GameState.Playing) {
            if (IsRightNote(noteIndex)) {
                notesSucceeded++;
                int points = notesSucceeded * 10;
                _playersManager.MakePoints(points);

                if (PhasesManager.CurrentState == GameState.Silence)
                {
                    Debug.Log("Made points in silence");
                }

                //Bonus points for a perfect solo
                if (notesSucceeded == _riffLength && !madeMistake) {
                    //Should be 200 for composer and 400 for the other, will change at some point
                    _playersManager.MakePoints(400);
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
                
                _playersManager.MakePoints(-penalty);
                AudioManager.Instance.PlaySound("WrongNote");
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
        if (PhasesManager.CurrentState == GameState.Playing || (PhasesManager.CurrentState == GameState.Silence && PhasesManager.NextState == GameState.Playing))
        {
            _currentTrackedNotes.Add(noteIcon);
        }
    }

    public void RemoveTrackedIcon(NoteIcon noteIcon) 
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
    
    public virtual void GameStateChanged()
    {
    }

    //Different implementation for DuelMode
    protected void PlayNoteSound(int noteIndex)
    {
        AudioManager.Instance.PlayNote(_playersManager.CurrentPlayer.index, noteIndex);
    }

    public void OnRPressed(int playerIndex, int currentPlayerIndex) 
    {
        if(_isPausingForEmptySolo && (playerIndex == -1 || playerIndex == currentPlayerIndex))
        {
            Time.timeScale = 1;

            PhasesManager.ChangeState(0);
            PhasesManager.ChangeNextState(1);
            UIScript.SetNextBar();
            UIScript.UpdateCountdown(0);

            _pauseMenu.TogglePauseMenuBehaviour();
            _isPausingForEmptySolo = false;
            UIScript.SetForgotRecordUI(false);
            UIScript.currentTracker = null;
        }
    }

    protected void EmptyRiffAlert() 
    {
        _isPausingForEmptySolo = true;
        //Call in duelMode
        _pauseMenu.TogglePauseMenuBehaviour();
        UIScript.SetForgotRecordUI(true);

        Time.timeScale = 0;
    }
}
