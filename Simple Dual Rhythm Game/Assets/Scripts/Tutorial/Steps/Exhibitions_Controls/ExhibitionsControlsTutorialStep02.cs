using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionsControlsTutorialStep02 : TutorialStep
{
    public override void ProcessEvent(EventType eventType, int code)
    {
        Debug.Log(code);

        if (eventType == EventType.NotePlayed)
        {
            OnCompleted();
        }
    }
}
