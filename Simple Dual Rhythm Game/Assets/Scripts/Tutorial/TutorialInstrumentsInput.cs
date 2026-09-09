using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TutorialInstrumentsInput : InstrumentsInput
{
    [SerializeField] private TutorialStepsManager _stepsManager;

    private void Awake()
    {
        base.Awake();

        if (_inputMode == InputMode.keytar)
        {
            SetUpInputRedirector(ProcessKeytarInput);
        }
    }

    public void SetManager(TutorialStepsManager stepsManager)
    {
        _stepsManager = stepsManager;
    }

    private void OnAny()
    {
        _stepsManager.AnyKeyPressed();
    }

    public new void ProcessKeytarInput(int device, int key, bool pressed)
    {
        base.ProcessKeytarInput(device, key, pressed);
        OnAny();
    }
}
