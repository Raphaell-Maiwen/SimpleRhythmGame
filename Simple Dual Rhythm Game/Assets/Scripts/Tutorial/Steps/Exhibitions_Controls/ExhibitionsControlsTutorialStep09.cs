using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionsControlsTutorialStep09 : TutorialStep
{
    private List<int> _fretsPressed = new List<int>();

    private void OnEnable()
    {
        _fretsPressed.Clear();
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
            if (_fretsPressed.Count > 1)
            {
                OnCompleted();
            }
            else
            {
                Debug.Log("That's not a chord!");
                //Show error message
            }
        }
    }
}
