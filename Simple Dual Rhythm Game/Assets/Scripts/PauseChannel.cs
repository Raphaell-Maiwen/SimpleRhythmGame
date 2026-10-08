using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseChannel : ScriptableObject
{
    public bool IsPaused = false;

    private void OnEnable()
    {
        IsPaused = false;
    }
}
