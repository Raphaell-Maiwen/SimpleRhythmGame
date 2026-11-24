using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Metronome : MonoBehaviour
{
    [SerializeField] private MetronomeEventChannel _metronomeEventChannel;
    [SerializeField] private Parameters _parameters;
    [SerializeField] private GameLoop _gameLoop;
    [SerializeField] private PauseChannel _pauseChannel;
    
    public int bpm;

    [HideInInspector]
    public float frequency;
    public int beatPerBar;
    public int bars = 1;
    private int metronomeCounter = 0;
    private int mod = 1;

    [SerializeField] private int _tutorialBPM = 60;
    [SerializeField] private int _tutorialbars = 1;

    private float initialTime;

    [Range(0, 1)]
    public float strongTick;
    [Range(0, 1)]
    public float weakTick;

    [SerializeField] private PlayersManager playersScript;

    bool firstTick;

    public Action<int,int> OnTempoChanged;
    public Action OnFirstTick;
    public Action<int> OnTick;
    public Action OnNextPhase;
    public Action OnNewBar;

    private void Awake() 
    {
        enabled = false;
        _metronomeEventChannel.RegisterMetronome(this);
    }

    public void StartGame()
    {
        firstTick = true;
        
        bpm = _parameters.bpm;
        beatPerBar = _parameters.beatPerBar;
        bars = _parameters.bars;

        SetUp();
        AudioManager.Instance.SetBeat("4/4Beat1");
        AudioManager.Instance.SetBeatSpeed(bpm);
        enabled = true;
        
        _pauseChannel._onGamePausedOrUnpaused.AddListener(OnGamePaused);
    }

    public void TutorialStartGame(TutorialStepsManager stepsManager, Action onfirstTick)
    {
        Debug.Log("TutorialStartGame");
        _gameLoop = stepsManager;
        firstTick = true;
        
        bpm = _tutorialBPM;
        bars = _tutorialbars;
        
        SetUp();
        AudioManager.Instance.SetBeat("4/4Beat1");
        AudioManager.Instance.SetBeatSpeed(bpm);
        enabled = true;
        
        OnFirstTick += onfirstTick;
        
        _pauseChannel._onGamePausedOrUnpaused.AddListener(OnGamePaused);
    }

    public void SetUp()
    {
        frequency = 60f / bpm;
        initialTime = Time.time;
        metronomeCounter = 0;
    }

    public PhasesManager SpawnPhasesManager(List<GameState> states)
    {
        PhasesManager phasesManager = gameObject.AddComponent<PhasesManager>();
        phasesManager.Init(states, this);
        return  phasesManager;
    }

    public void ChangeTempo()
    {
        frequency = 60f / bpm;
        
        metronomeCounter = 0;
        mod = 0;
        initialTime = Time.time;
        
        OnTempoChanged?.Invoke(bpm, beatPerBar);
        AudioManager.Instance.SetBeatSpeed(bpm);
    }

    void Tick()
    {
        //Add logs here
        if (firstTick) {
            firstTick = false;
            OnFirstTick?.Invoke();
        }

        //Check if we're at the beginning of a new bar
        if (metronomeCounter % beatPerBar == 0 && _gameLoop.GetEntryGatePosition())
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

    public void IncreaseMetronomeCounter()
    {
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
            AudioManager.Instance.PauseBeat();
        }
        else {
            AudioManager.Instance.UnPauseBeat();
        }
    }

    

    public void EndGame()
    {
        enabled = false;
    }
}