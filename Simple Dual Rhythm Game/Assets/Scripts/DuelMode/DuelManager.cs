using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DuelManager : GameManager{
    private int soloIndex;
    [SerializeField] private int solosToDo;
    [SerializeField] private Metronome metronomeScript;
    [SerializeField] private DuelMode _duelMode;

    public void AddSolo() 
    {
        soloIndex++;
        if (soloIndex == solosToDo - 1)
            LastSolo();
        else if (soloIndex == solosToDo + 1)
            EndOfGame();
    }

    private new void Awake() 
    {
        base.Awake();

        //So that each player plays the required amount of solos
        solosToDo *= 2;
        startGame.AddListener(metronomeScript.StartGame);
    }

    void LastSolo() 
    {
        metronomeScript.bpm += (metronomeScript.bpm / 5);
        metronomeScript.ChangeTempo();
        _duelMode.SetLastSolo();
        //TODO: Change visuals?
    }
}