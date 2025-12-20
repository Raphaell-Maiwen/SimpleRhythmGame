using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DuelManager : GameManager{
    private int soloIndex;
    [SerializeField] private int solosToDo;
    [SerializeField] private Metronome metronomeScript;
    [SerializeField] private GameObject _flames;

    public void AddSolo() 
    {
        soloIndex++;
        if (soloIndex == solosToDo - 1)
            LastSolo();
        else if (soloIndex == solosToDo + 1)
            EndOfGame();
    }

    private void Awake() 
    {
        base.Awake();

        //So that each player plays the required amount of solos
        solosToDo *= 2;
        startGame.AddListener(metronomeScript.StartGame);
    }

    private void Start()
    {
        base.Start();

        if (_parameters.bars > 1) 
        { 
            _flames.SetActive(false);
        }
    }

    void LastSolo() 
    {
        metronomeScript.bpm += (metronomeScript.bpm / 5);
        metronomeScript.ChangeTempo();
        metronomeScript.SetLastSolo();
        //TODO: Change visuals?
    }
}