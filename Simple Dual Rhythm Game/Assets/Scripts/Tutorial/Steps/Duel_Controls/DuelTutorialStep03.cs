using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelTutorialStep03 : TutorialStep
{
    [SerializeField] private MetronomeEventChannel _metronomeEventChannel;
    //An event channel to talk to the part ui?
    
    private bool _playedANote;

    public override void Start()
    {
        base.Start();
        _metronomeEventChannel.OnStartTutorial(_stepsManager);
    }

    public override void ProcessEvent(EventType eventType, int code = -1)
    {
        if (eventType == EventType.NotePlayed && PhasesManager.CurrentState == GameState.Playing)
        {
            _playedANote = true;
        }
        else if (eventType == EventType.BarFinished)
        {
            if (_playedANote)
            {
                OnCompleted();
            }
            else
            {
                Debug.Log("You did not play a note");
                //Do the whole press R thingy.
            }
        }
    }
}
