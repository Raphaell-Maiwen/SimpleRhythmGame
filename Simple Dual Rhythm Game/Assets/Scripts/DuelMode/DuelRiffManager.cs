using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelRiffManager : RiffManager
{
    public bool _isLastRiff;
    private DuelMode _duelMode;

    private void Awake()
    {
        OnNotePlayed += PlayNoteSound;
    }

    private void OnDestroy()
    {
        OnNotePlayed -= PlayNoteSound;
    }

    public void Init(PlayersManager playersManager, PartUI partUI, PauseMenu pauseMenu, DuelMode duelMode)
    {
        base.Init(playersManager, partUI, pauseMenu);
        _duelMode = duelMode;
    }

    protected new void PlayNoteSound(int noteIndex) {
        if (!_isLastRiff)
        {
            AudioManager.Instance.PlayNote(_playersManager.CurrentPlayer.index, noteIndex);
        }
        else
        {
            AudioManager.Instance.PlayFinaleNote(_playersManager.CurrentPlayer.index, noteIndex);
        }
    }

    public override void GameStateChanged()
    {
        base.GameStateChanged();
        
        if (PhasesManager.CurrentState == GameState.Recording && _riffLength == 0)
        {
            EmptyRiffAlert();
            //TODO: Make metronome static???
            //_duelMode.Metronome.IncreaseMetronomeCounter();
            return;
        }

        PhasesManager.ChangeState(PhasesManager.CurrentStateIndex + 1);
        PhasesManager.ChangeNextState(PhasesManager.NextStateIndex + 1);
        
        //Reset the riff if we're recording again, change player if it's a silence...
        //TODO: How to take the error margin into account?
        if (PhasesManager.CurrentState == GameState.ChangePlayer) {
            _playersManager.changeCurrentPlayer();
            PhasesManager.ChangeState(PhasesManager.CurrentStateIndex + 1);
        }
        if (PhasesManager.NextState == GameState.Recording) {
            _riffLength = 0;
        }
        else if(PhasesManager.CurrentState != GameState.Playing) {
            UIScript.UnPlayedAllNotes();
        }
        
        //A generic "phase changed" event

        if (_duelMode.IsGameEnded) 
        {
            return;
        }

        //Conditional here? Or called from PhasesManager event???
        UIScript.SetNextBar();

        //Change this code slightly when supporting multiple bars
        notesSucceeded = 0;
        madeMistake = false;
    }
}
