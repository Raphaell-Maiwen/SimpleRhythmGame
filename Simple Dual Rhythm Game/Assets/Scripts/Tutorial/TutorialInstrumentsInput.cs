using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TutorialInstrumentsInput : InstrumentsInput
{
    private List<int> _keysPressed = new List<int>();
    
    private TutorialStepsManager _stepsManager;

    protected override void Awake()
    {
        base.Awake();

        if (_inputMode == InputMode.keytar)
        {
            SetUpInputRedirector(ProcessKeytarInput);
        }
    }

    protected override void Start()
    {
        base.Start();
        
        OnFretPressed += _stepsManager.OnFretPressed;
        OnFretReleased += _stepsManager.OnFretReleased;
    }

    public void SetManager(TutorialStepsManager stepsManager)
    {
        _stepsManager = stepsManager;
    }

    //Double check that this triggers correctly for a simple keyboard
    private void OnAny()
    {
        _stepsManager.AnyKeyPressed();
    }

    public new bool ProcessKeytarInput(int device, int key, bool pressed)
    {
        bool madeAnAction = base.ProcessKeytarInput(device, key, pressed);

        if (pressed && !_keysPressed.Contains(key) && !madeAnAction)
        {
            OnAny();
            _keysPressed.Add(key);
        }
        else if (!pressed)
        {
            _keysPressed.Remove(key);
        }
        
        return madeAnAction;
    }
}
