using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour{
    public int soloIndex;
    public int solosToDo;
    [SerializeField] private Metronome metronomeScript;
    public UnityEvent startGame;

    public void AddSolo() {
        soloIndex++;
        if (soloIndex == solosToDo - 1)
            LastSolo();
        else if (soloIndex == solosToDo + 1)
            EndOfGame();
    }

    private void Awake() {
        //So that each player plays the required amount of solos
        solosToDo *= 2;
        
        startGame.AddListener(metronomeScript.StartGame);
    }

    private void Start()
    {
        if (Parameters.instance.inputMode == InputMode.keytar)
        {
            Debug.Log("Register keyboard 1 and 2");
        }
        else
        {
            startGame?.Invoke();
        }
    }

    void LastSolo() {
        metronomeScript.bpm += (metronomeScript.bpm / 5);
        metronomeScript.ChangeTempo();
        //Change visuals?
    }

    void EndOfGame() {
        Time.timeScale = 0f;

        AudioSource[] sounds = GameObject.FindObjectsOfType<AudioSource>();

        foreach (AudioSource sound in sounds) {
            sound.Stop();
        }

        print("end");
    }
}