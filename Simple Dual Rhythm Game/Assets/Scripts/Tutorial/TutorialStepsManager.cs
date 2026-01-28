using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStepsManager : GameLoop
{
    [SerializeField] private TutorialStep[] _tutorialSteps;
    private TutorialStep _currentStep;
    private int _currentStepIndex;

    [SerializeField] private TextMeshProUGUI _instructions;
    [SerializeField] private GameObject _panel;

    private void Start()
    {
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

        if (_currentStep.Instructions != "")
        {
            _panel.SetActive(true);
            _panel.transform.position = _currentStep.PanelPos;
            _instructions.text = _currentStep.Instructions;
        }
        else
        {
            _panel.SetActive(false);
        }

        _currentStepIndex++;
    }

    private void NotifyStep(EventType eventType, int code) 
    {
        _currentStep.ProcessEvent(eventType, code);
    }

    private void NotifyStep(EventType eventType)
    {
        _currentStep.ProcessEvent(eventType);
    }

    public override void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex)
    {
        if (currentPlayerIndex == playerIndex)
        {
            NotifyStep(EventType.NotePlayed, noteIndex);
        }
    }

    public void AnyKeyPressed()
    {
        NotifyStep(EventType.AnyKeyPressed);
    }

    //A function for frets without strum

    private void EndOfTutorial()
    {
        Debug.Log("End of tutorial");
    }
}

public enum EventType 
{ 
    AnyKeyPressed,
    FretPressed,
    NotePlayed,
    SuccessfulNote, //One will have a private int with amount of notes to play?
    BarFinished
}
