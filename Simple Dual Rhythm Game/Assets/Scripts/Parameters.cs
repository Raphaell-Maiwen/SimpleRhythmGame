using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters: MonoBehaviour
{
    public static Parameters instance;

    public int bpm;
    public int beatPerBar;
    public int bars;

    public InputMode inputMode;
    
    //Control schemes, sounds...

    private void Awake() {
        DontDestroyOnLoad(gameObject);

        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
}

public enum InputMode 
{
    gamepad,
    keyboard,
    keytar
}