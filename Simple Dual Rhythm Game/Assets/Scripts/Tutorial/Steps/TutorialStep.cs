using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    [SerializeField] private TutorialStepData _stepData;
    public TutorialStepData StepData => _stepData;
    protected TutorialStepsManager _stepsManager;

    public void Init(TutorialStepsManager tutorialStepsManager)
    { 
        _stepsManager = tutorialStepsManager;
    }

    public abstract void ProcessEvent(EventType eventType, int code = -1);
    public void OnCompleted()
    {
        _stepsManager.IncrementStep();
    }
}
