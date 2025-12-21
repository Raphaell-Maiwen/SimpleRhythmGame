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
    //Enum short medium long?
    public int numberOfNotes;
    public int numberOfPlayers;
}

public enum InputMode 
{
    gamepad,
    keyboard,
    keytar
}