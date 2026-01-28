using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTutorialStep01 : TutorialStep
{
    private int aNumber;

    public override void ProcessEvent(EventType eventType, int code)
    {
        Debug.Log("Getting there!");
        aNumber++;

        if (aNumber == 5)
        {
            OnCompleted();
        }
    }
}
