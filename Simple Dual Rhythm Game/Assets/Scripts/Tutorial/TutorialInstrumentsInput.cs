using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class TutorialInstrumentsInput : InstrumentsInput
{
    private List<int> _keysPressed = new List<int>();
    
    [SerializeField] private TutorialStepsManager _stepsManager;

    private new void Awake()
    {
        base.Awake();

        if (_inputMode == InputMode.keytar)
        {
            SetUpInputRedirector(ProcessKeytarInput);
            
            OnFretPressed += _stepsManager.OnFretPressed;
            OnFretReleased += _stepsManager.OnFretReleased;
        }
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
            Debug.Log(key + " pressed");
            OnAny();
            _keysPressed.Add(key);
        }
        else if (!pressed)
        {
            Debug.Log(key + " released");
            _keysPressed.Remove(key);
        }
        
        return madeAnAction;
    }
}
