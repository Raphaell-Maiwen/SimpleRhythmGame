using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep01 : TutorialStep
{
    private int aNumber;

    public override void OnCompleted()
    {
        Debug.Log("Bravo!");
        _stepsManager.IncrementStep();
    }

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
