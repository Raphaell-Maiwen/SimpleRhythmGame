using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    //Add text variable n stuff
    protected TutorialStepsManager _stepsManager;

    public void Init(TutorialStepsManager tutorialStepsManager)
    { 
        _stepsManager = tutorialStepsManager;
    }

    public abstract void ProcessEvent(EventType eventType, int code);
    public abstract void OnCompleted();
}
