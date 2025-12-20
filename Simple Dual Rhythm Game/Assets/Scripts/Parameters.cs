using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters: ScriptableObject
{
    public int bpm;
    public int beatPerBar;
    public int bars;

    public InputMode inputMode;
    public bool fKeysOn;

    //For Race Mode
    public int numberOfNotes;
}

public enum InputMode 
{
    gamepad,
    keyboard,
    keytar
}