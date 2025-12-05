using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour{
    public int soloIndex;
    public int solosToDo;
    [SerializeField] private Metronome metronomeScript;
    [SerializeField] private Parameters _parameters;
    [SerializeField] private EndOfGameScreen _endOfGameScreen;
    [SerializeField] private PlayersManager _playersManager;
    [SerializeField] private InstrumentsInput _instrumentsInputPrefab;
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

        if (_parameters.inputMode == InputMode.keytar)
        {
            Instantiate(_instrumentsInputPrefab);
        }
    }

    private void Start()
    {
        if (_parameters.inputMode != InputMode.keytar) {
            startGame?.Invoke();
        }
    }

    void LastSolo() {
        metronomeScript.bpm += (metronomeScript.bpm / 5);
        metronomeScript.ChangeTempo();
        //TODO: Change visuals?
    }

    void EndOfGame() {
        Time.timeScale = 0f;

        AudioSource[] sounds = GameObject.FindObjectsOfType<AudioSource>();

        foreach (AudioSource sound in sounds) {
            sound.Stop();
        }   
        
        _endOfGameScreen.Init(_playersManager.DeclareWinner());
        _endOfGameScreen.gameObject.SetActive(true);
    }
}