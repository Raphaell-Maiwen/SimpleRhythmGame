using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelTutorialStep00 : TutorialStep
{
    public override void ProcessEvent(EventType eventType, int code = -1)
    {
        if (eventType == EventType.AnyKeyPressed)
        {
            OnCompleted();
        }
    }
}
