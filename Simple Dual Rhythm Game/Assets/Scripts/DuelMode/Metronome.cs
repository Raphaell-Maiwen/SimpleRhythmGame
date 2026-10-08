using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Metronome : MonoBehaviour
{
    [SerializeField] private Parameters _parameters;
    
    [SerializeField] private DuelMode _duelMode;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private PauseMenu _pauseMenu;
    
    [SerializeField] private PartUI UIScript;
    
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

    [SerializeField] private PlayersManager playersScript;

    bool firstTick = true;

    private bool _isPausingForEmptySolo;
    
    public Action<int> OnTick;
    public Action OnNextPhase;
    public Action OnNewBar;

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

        //THIS
        UIScript.ChangeTempo(bpm, beatPerBar);
        _audioManager.SetBeatSpeed(bpm);
    }

    void Tick()
    {
        if (firstTick) {
            firstTick = false;
            _audioManager.PlayBeat();
        }

        //Check if we're at the beginning of a new bar
        if (metronomeCounter % beatPerBar == 0 && _duelMode.GetEntryGatePosition())
        {
            OnNewBar?.Invoke();
        }
        //Check if we're at the beginning of a new cycle
        else if (metronomeCounter % (beatPerBar * bars + beatPerBar) == 0)
        {
            OnNextPhase?.Invoke();
        }

        OnTick?.Invoke(metronomeCounter % beatPerBar);

        //TODO: Reset counter at 0 instead??
        metronomeCounter++;
    }

    void Update()
    {
        float timeSpent = Time.time - initialTime;

        if (timeSpent >= frequency * metronomeCounter + mod) {
            Tick();
        }
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

    private void EmptyRiffAlert() 
    {
        /*_isPausingForEmptySolo = true;
        _pauseMenu.TogglePauseMenuBehaviour();
        UIScript.SetForgotRecordUI(true);

        Time.timeScale = 0;*/
    }

    //Double-check that this doesn't interact with regular pause
    public void OnRPressed(int playerIndex, int currentPlayerIndex) 
    {
        if(_isPausingForEmptySolo && (playerIndex == -1 || playerIndex == currentPlayerIndex))
        {
            Time.timeScale = 1;

            _duelMode.ChangeState(0);
            //ChangeNextState(1);
            //SetNextBar();

            _pauseMenu.TogglePauseMenuBehaviour();
            _isPausingForEmptySolo = false;
            /*UIScript.SetForgotRecordUI(false);
            UIScript.currentTracker = null;*/
        }
    }
}