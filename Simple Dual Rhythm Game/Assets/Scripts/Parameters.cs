﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters: ScriptableObject
{
    public int bpm;
    public int beatPerBar;
    public int bars;

    public InputMode inputMode;
}

public enum InputMode 
{
    gamepad,
    keyboard,
    keytar
}