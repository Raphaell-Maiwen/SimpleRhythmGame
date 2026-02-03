using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInstrumentsInput : InstrumentsInput
{
    [SerializeField] private TutorialStepsManager _stepsManager;

    public void SetManager(TutorialStepsManager stepsManager)
    {
        _stepsManager = stepsManager;
    }

    private void OnAny()
    {
        Debug.Log("Any!");
        _stepsManager.AnyKeyPressed();
    }
}
