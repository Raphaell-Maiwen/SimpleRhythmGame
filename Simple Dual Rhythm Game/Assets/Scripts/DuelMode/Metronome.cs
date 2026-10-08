using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Metronome : MonoBehaviour
{
    [SerializeField] private Parameters _parameters;
    [SerializeField] private DuelManager _gameManager;
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
    
    private bool _isLastSolo;

    private bool _isPausingForEmptySolo;
    private bool _isGameEnded;
    
    public Action<int> OnTick;
    public Action OnNextPhase;

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
        _gameManager.stopGame.AddListener(EndGame);
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

        UIScript.ChangeTempo(bpm, beatPerBar);
        _audioManager.SetBeatSpeed(bpm);
    }

    public void SetLastSolo()
    {
        _isLastSolo = true;
    }

    public void EndGame() 
    {
        _isGameEnded = true;
        enabled = false;
    }

    void Tick()
    {
        if (firstTick) {
            firstTick = false;
            _audioManager.PlayBeat();
        }

        //Check if we're at the beginning of a new bar
        if (metronomeCounter % beatPerBar == 0 && _duelMode.currentState == GameState.Silence)
        {
            Debug.Log("New Bar");
            ChangeState(_duelMode.currentStateIndex + 1);
            ChangeNextState(_duelMode.nextStateIndex + 1);
            
            bool playAgain = GetPreviousState(_duelMode.currentStateIndex) == GameState.ChangePlayer;
            
            UIScript.ClearCountdown();
            UIScript.ChangeNextStateMessage(_duelMode.nextState, playAgain);
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

    public void ChangeState(int newStateIndex)
    {
        _duelMode.currentStateIndex = newStateIndex;

        if (_duelMode.currentStateIndex == _duelMode.statesSeries.Length)
        {
            _duelMode.currentStateIndex = 0;
            UIScript.EraseAllNotes();
            _gameManager.AddSolo();
        }

        _duelMode.currentState = _duelMode.statesSeries[_duelMode.currentStateIndex];
    }

    public void ChangeNextState(int newStateIndex)
    {
        _duelMode.nextStateIndex = newStateIndex;
        
        if (_duelMode.nextStateIndex == _duelMode.statesSeries.Length)
        {
            _duelMode.nextStateIndex = 0;
        }
        
        _duelMode.nextState = _duelMode.statesSeries[_duelMode.nextStateIndex];
        
         if (_duelMode.nextState == GameState.ChangePlayer)
        {
            _duelMode.nextStateIndex++;
            _duelMode.nextState = _duelMode.statesSeries[_duelMode.nextStateIndex];
        }
    }

    GameState GetPreviousState(int newStateIndex)
    {
        int previousStateIndex = newStateIndex;
        previousStateIndex--;

        if (previousStateIndex < 0)
        {
            previousStateIndex = _duelMode.statesSeries.Length - 1;
        }
        
        return  _duelMode.statesSeries[previousStateIndex];
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

            ChangeState(0);
            ChangeNextState(1);
            //SetNextBar();

            _pauseMenu.TogglePauseMenuBehaviour();
            _isPausingForEmptySolo = false;
            /*UIScript.SetForgotRecordUI(false);
            UIScript.currentTracker = null;*/
        }
    }
}