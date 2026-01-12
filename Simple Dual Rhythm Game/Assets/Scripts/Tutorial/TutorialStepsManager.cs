using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TutorialStepsManager : GameLoop
{
    [SerializeField] private TutorialStep[] _tutorialSteps;
    private TutorialStep _currentStep;
    private int _currentStepIndex;

    private void Start()
    {
        //Add all the listeners here
        IncrementStep();
    }

    public void IncrementStep() 
    {
        if (_currentStep != null)
        { 
            _currentStep.gameObject.SetActive(false);
        }

        if (_currentStepIndex >= _tutorialSteps.Length)
        {
            EndOfTutorial();
            return;
        }

        _currentStep = _tutorialSteps[_currentStepIndex];
        _currentStep.gameObject.SetActive(true);
        _currentStep.Init(this); //One will spawn bars, other notes, etc.
        _currentStepIndex++;
    }

    private void NotifyStep(EventType eventType, int code) 
    {
        _currentStep.ProcessEvent(eventType, code);
    }

    public override void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex)
    {
        if (currentPlayerIndex == playerIndex)
        {
            NotifyStep(EventType.KeyPressed, noteIndex);
        }
    }

    private void EndOfTutorial()
    {
        Debug.Log("End of tutorial");

    }
}

public enum EventType 
{ 
    KeyPressed,
    SuccessfulNote, //One will have a private int with amount of notes to play?
    BarFinished
}
