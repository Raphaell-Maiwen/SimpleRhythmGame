using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialStepsManager : GameLoop
{
    [SerializeField] private TutorialConfig _testTutorialConfig;
    [SerializeField] private TutorialConfigChannel _tutorialConfigChannel;
    private List<GameObject> _tutorialSteps = new List<GameObject>();
    private TutorialStep _currentStep;
    private int _currentStepIndex;

    [SerializeField] private TextMeshProUGUI _instructions;
    [SerializeField] private RectTransform _panel;
    [SerializeField] private GameObject _pressAnyKeyPrompt;

    [SerializeField] private TextMeshProUGUI _errorMsg;
    [SerializeField] private RectTransform _errorMsgPanel;
    
    //Later todo cleanup: remove list from individuals steps
    private List<int> _keysPressed = new List<int>();

    private void Start()
    {
        _tutorialSteps.Clear();
        
        #if UNITY_EDITOR
        if (!_tutorialConfigChannel.GetConfig())
        {
            foreach (var step in _testTutorialConfig.TutorialSteps)
            {
                var instantiatedStep = Instantiate(step.gameObject);
                _tutorialSteps.Add(instantiatedStep);
            }
            
            IncrementStep();
            return;
        }
        #endif

        foreach (var step in _tutorialConfigChannel.GetConfig().TutorialSteps)
        {
            var instantiatedStep = Instantiate(step.gameObject);
            _tutorialSteps.Add(instantiatedStep);
        }
        
        IncrementStep();
    }

    public void InjectManager(PlayerInput input)
    {
        input.GetComponent<TutorialInstrumentsInput>().SetManager(this);
    }

    public void IncrementStep() 
    {
        if (_currentStep != null)
        { 
            _currentStep.gameObject.SetActive(false);
        }

        if (_currentStepIndex >= _tutorialSteps.Count)
        {
            EndOfTutorial();
            return;
        }

        _currentStep = _tutorialSteps[_currentStepIndex].GetComponent<TutorialStep>();
        _currentStep.gameObject.SetActive(true);
        _currentStep.Init(this); //One will spawn bars, other notes, etc.

        var stepData = _currentStep.StepData;

        if (stepData.Instructions != "")
        {
            _panel.gameObject.SetActive(true);
            _panel.anchoredPosition = stepData.PanelPos;
            _instructions.text = stepData.Instructions;
            _errorMsgPanel.transform.localPosition = stepData.ErrorMsgPos;

            if (stepData.PressAnyKeyToContinueWindow)
            {
                _pressAnyKeyPrompt.SetActive(true);
            }
            else
            {
                _pressAnyKeyPrompt.SetActive(false);
            }
        }
        else
        {
            _panel.gameObject.SetActive(false);
        }
        
        _errorMsgPanel.gameObject.SetActive(false);
        _errorMsg.text = "";

        _currentStepIndex++;
        
        _errorMsgPanel.anchoredPosition = stepData.ErrorMsgPos;
    }

    public void ShowErrorMessage(string message)
    {
        _errorMsg.text = message;
        _errorMsgPanel.gameObject.SetActive(true);
    }

    public void HideErrorMessage()
    {
        _errorMsg.text = "";
        _errorMsgPanel.gameObject.SetActive(false);
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
        //Something to do with currentPlayerIndex and playerIndex
        NotifyStep(EventType.NotePlayed, noteIndex);
        
        _audioManager.PlayNote(playerIndex, noteIndex);
    }

    public void OnFretPressed(int noteIndex, int playerIndex)
    {
        //Something to do with currentPlayerIndex and playerIndex
        NotifyStep(EventType.FretPressed, noteIndex);
    }
    
    public void OnFretReleased(int noteIndex, int playerIndex)
    {
        NotifyStep(EventType.FretReleased, noteIndex);
    }

    public void AnyKeyPressed()
    {
        NotifyStep(EventType.AnyKeyPressed);
    }

    private void EndOfTutorial()
    {
#if UNITY_EDITOR
        if (!_tutorialConfigChannel.GetConfig())
        {
            SceneManager.LoadScene(_testTutorialConfig.SceneToLoad);
            return;
        }
#endif
        SceneManager.LoadScene(_tutorialConfigChannel.GetConfig().SceneToLoad);;
    }
}

public enum EventType 
{ 
    AnyKeyPressed,
    FretPressed,
    FretReleased,
    NotePlayed,
    SuccessfulNote, //One will have a private int with amount of notes to play?
    BarFinished
}
