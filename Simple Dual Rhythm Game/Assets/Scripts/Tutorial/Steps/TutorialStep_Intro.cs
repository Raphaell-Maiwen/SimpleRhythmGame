using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep_Intro : TutorialStep
{
    public override void ProcessEvent(EventType eventType, int code = -1)
    {
        if (eventType == EventType.AnyKeyPressed)
        {
            Debug.Log("First step done.");
            OnCompleted();
        }
    }
}
