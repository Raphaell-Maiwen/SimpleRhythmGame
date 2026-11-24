using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseChannel : ScriptableObject
{
    public bool IsPaused = false;
    public UnityEvent<bool> _onGamePausedOrUnpaused;

    private void OnEnable()
    {
        IsPaused = false;
    }

    public void OnPauseToggle(bool isPaused)
    {
        _onGamePausedOrUnpaused.Invoke(isPaused);
    }
}
