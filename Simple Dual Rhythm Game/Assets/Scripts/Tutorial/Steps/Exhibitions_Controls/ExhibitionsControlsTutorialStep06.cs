using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionsControlsTutorialStep06 : TutorialStep
{
    //Add some feedback messages
    //Add some visual progression
    private List<int> _fretsPressed = new List<int>();

    private List<int> _notesPlayed = new List<int>();

    private void OnEnable()
    {
        _fretsPressed.Clear();
        _notesPlayed.Clear();
    }

    public override void ProcessEvent(EventType eventType, int code)
    {
        if (eventType == EventType.FretReleased)
        {
            _fretsPressed.Remove(code);
        }
        else if (eventType == EventType.FretPressed)
        {
            if (!_fretsPressed.Contains(code))
            {
                _fretsPressed.Add(code);
            }
        }
        else if (eventType == EventType.NotePlayed)
        {
            if (_fretsPressed.Count <= 1)
            {
                if (!_notesPlayed.Contains(code))
                {
                    //Add visual feedback here
                    _notesPlayed.Add(code);
                }
            }
            else if (_fretsPressed.Count > 1)
            {
                Debug.Log("Non non, that's a chord.");
                //Show error message
            }
        }

        if(_notesPlayed.Count == 4) 
        {
            OnCompleted();
        }
    }
}
