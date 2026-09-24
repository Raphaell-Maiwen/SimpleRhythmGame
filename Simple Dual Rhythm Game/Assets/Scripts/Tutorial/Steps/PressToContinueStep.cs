using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToContinueStep : TutorialStep
{
    public override void ProcessEvent(EventType eventType, int code)
    {
        if (eventType == EventType.AnyKeyPressed)
        {
            OnCompleted();
        }
    }
}
