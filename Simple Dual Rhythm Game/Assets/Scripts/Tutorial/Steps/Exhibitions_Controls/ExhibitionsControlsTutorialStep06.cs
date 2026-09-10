using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionsControlsTutorialStep06 : TutorialStep
{
    //Add some feedback messages
    //Add some visual progression
    private int fretsPressed = 0;

    private List<int> _notesPlayed = new List<int>();

    private void OnEnable()
    {
        _notesPlayed.Clear();
    }

    public override void ProcessEvent(EventType eventType, int code)
    {
        if (eventType == EventType.FretReleased)
        {
            fretsPressed = Mathf.Max(0, fretsPressed - 1);
        }
        else if (eventType == EventType.FretPressed)
        {
            fretsPressed++;
        }
        else if (eventType == EventType.NotePlayed)
        {
            if (fretsPressed == 1)
            {
                if (!_notesPlayed.Contains(code))
                {
                    //Add visual feedback here
                    _notesPlayed.Add(code);
                }
            }
            else if (fretsPressed > 1)
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
