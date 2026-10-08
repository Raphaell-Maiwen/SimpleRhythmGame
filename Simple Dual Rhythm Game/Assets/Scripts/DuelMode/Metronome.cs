using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Metronome : MonoBehaviour
{
    [SerializeField] private Parameters _parameters;
    
    //AudioManager should be a singleton or something of the likes?
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private PauseMenu _pauseMenu;
    
    public int bpm;

    [HideInInspector]
    public float frequency;
    public int beatPerBar;
    public int bars = 1;
    private int metronomeCounter = 0;
    private int mod = 1;

    private float initialTime;

    [Range(0, 1)]
    public float strongTick;
    [Range(0, 1)]
    public float weakTick;
    
    bool firstTick = true;

    public Action OnFirstTick;
    public Action<int> OnTick;
    public Action OnNewCycle;
    public Action OnNewBar;
    public Action<int, int> OnTempoChanged;

    private void Awake() 
    {
        enabled = false;
    }

    public void StartGame()
    {
        bpm = _parameters.bpm;
        beatPerBar = _parameters.beatPerBar;
        bars = _parameters.bars;

        SetUp();
        _audioManager.SetBeat("4/4Beat1");
        _audioManager.SetBeatSpeed(bpm);
        enabled = true;

        //Cleaner: register itself through a SO?
        _pauseMenu._onGamePausedOrUnpaused.AddListener(OnGamePaused);
    }

    public void SetUp()
    {
        frequency = 60f / bpm;
        
        initialTime = Time.time;
        metronomeCounter = 0;
    }

    public void ChangeTempo()
    {
        frequency = 60f / bpm;
        
        metronomeCounter = 0;
        mod = 0;
        initialTime = Time.time;
        
        _audioManager.SetBeatSpeed(bpm);
        OnTempoChanged?.Invoke(bpm, beatPerBar);
    }

    void Tick()
    {
        if (firstTick) {
            firstTick = false;
            OnFirstTick?.Invoke();
        }

        //Check if we're at the beginning of a new bar
        if (metronomeCounter % beatPerBar == 0)
        {
            OnNewBar?.Invoke();
        }
        //Check if we're at the beginning of a new cycle
        else if (metronomeCounter % (beatPerBar * bars + beatPerBar) == 0)
        {
            OnNewCycle?.Invoke();
        }
        
        OnTick?.Invoke(metronomeCounter % beatPerBar);
        
        metronomeCounter++;
    }

    void Update()
    {
        float timeSpent = Time.time - initialTime;

        if (timeSpent >= frequency * metronomeCounter + mod) {
            Tick();
        }
    }

    public void IncreaseMetronomeCounter()
    {
        metronomeCounter++;
    }

    private void OnGamePaused(bool paused) 
    {
        if (paused)
        {
            _audioManager.PauseBeat();  
        }
        else {
            _audioManager.UnPauseBeat();
        }
    }

    
}