using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    //Add text variable n stuff
    protected TutorialStepsManager _stepsManager;

    [SerializeField] private Vector2 _panelPos;
    public Vector2 PanelPos => _panelPos;
    [SerializeField] private string _instructions;
    public string Instructions => _instructions;

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
