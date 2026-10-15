using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class DuelManager : GameManager{
    private int riffIndex;
    [FormerlySerializedAs("solosToDo")]
    [SerializeField] private int riffsToDo;
    [SerializeField] private Metronome metronomeScript;
    [SerializeField] private DuelMode _duelMode;

    public void AddRiff() 
    {
        riffIndex++;
        if (riffIndex == riffsToDo - 1)
            LastRiff();
        else if (riffIndex == riffsToDo + 1)
            EndOfGame();
    }

    private void Awake() 
    {
        base.Awake();

        //So that each player plays the required amount of solos
        riffsToDo *= 2;
        startGame.AddListener(metronomeScript.StartGame);
    }

    void OnDestroy()
    {
        startGame.RemoveListener(metronomeScript.StartGame);
    }

    void LastRiff() 
    {
        metronomeScript.bpm += (metronomeScript.bpm / 5);
        metronomeScript.ChangeTempo();
        _duelMode.SetLastRiff();
    }
}