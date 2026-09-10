using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionsControlsTutorialStep09 : TutorialStep
{
    private int fretsPressed = 0;

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
            if (fretsPressed > 1)
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
