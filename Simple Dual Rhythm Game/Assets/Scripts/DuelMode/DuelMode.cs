using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelMode : GameLoop
{
    [SerializeField] private DuelRiffManager _riffManager;
    [SerializeField] private Parameters _parameters;
    [SerializeField] private PartUI UIScript;
    [SerializeField] private Metronome _metronome;
    public Metronome Metronome => _metronome;
    [SerializeField] private DuelManager _gameManager;
    [SerializeField] private PlayersManager playersScript;
    [SerializeField] private List<GameState> statesSeries = new List<GameState>();
    
    private PhasesManager _phasesManager;
    
    private bool _isGameEnded;
    public bool  IsGameEnded => _isGameEnded;

    private void Awake()
    {
        _riffManager = gameObject.AddComponent<DuelRiffManager>();
        _riffManager.Init(playersScript, UIScript, _pauseMenu, this);
        
        SetUpMetronome();
        SetUpPhasesManager();

        _riffManager.OnNotePlayed += UIScript.DrawNewNote;
    }

    private void OnDestroy()
    {
        _riffManager.OnNotePlayed -= UIScript.DrawNewNote;
        
        _metronome.OnNextPhase -= _riffManager.GameStateChanged;
        _metronome.OnFirstTick -= FirstTick;
        _metronome.OnTick -= Tick;
        _metronome.OnNewBar -= NewBar;
        _metronome.OnTempoChanged -= ChangeTempo;

        _metronome.OnNewBar -= UIScript.NewBar;
        
        PhasesManager.OnAllPhasesCycledThrough -= UIScript.EraseAllNotes;
        PhasesManager.OnAllPhasesCycledThrough -= _gameManager.AddRiff;
    }

    private void SetUpMetronome()
    {
        _metronome.OnNextPhase += _riffManager.GameStateChanged;
        _metronome.OnFirstTick += FirstTick;
        _metronome.OnTick += Tick;
        _metronome.OnNewBar += NewBar;
        _metronome.OnTempoChanged += ChangeTempo;

        _metronome.OnNewBar += UIScript.NewBar;
        //_metronome.OnNextPhase += UIScript.SetNextBar;
    }

    private void SetUpPhasesManager()
    {
        _phasesManager = _metronome.SpawnPhasesManager(statesSeries);
        PhasesManager.OnAllPhasesCycledThrough += UIScript.EraseAllNotes;
        PhasesManager.OnAllPhasesCycledThrough += _gameManager.AddRiff;
    }
    
    private void Start()
    {
        UIScript.SetUp(_parameters.bpm, _parameters.beatPerBar, _riffManager.AddTrackedNote, _riffManager.RemoveTrackedIcon);
        _gameManager.stopGame.AddListener(EndGame);
    }

    public override void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex) 
    {
        _riffManager.PlayNote(noteIndex, playerIndex, currentPlayerIndex);
    }

    public override bool GetEntryGatePosition()
    {
        return PhasesManager.CurrentState == GameState.Silence;
    }

    

    private void Tick(int beat)
    {
        if (PhasesManager.CurrentState == GameState.Silence)
        {
            UIScript.UpdateCountdown(beat);
        }
    }

    private void FirstTick()
    {
        AudioManager.Instance.PlayBeat();
    }

    private void NewBar()
    {
        PhasesManager.ChangeState(PhasesManager.CurrentStateIndex + 1);
        PhasesManager.ChangeNextState(PhasesManager.NextStateIndex + 1);
    }

    //Double-check that this doesn't interact with regular pause
    public void OnRPressed(int playerIndex, int currentPlayerIndex) 
    {
        //Send an event?
        _riffManager.OnRPressed(playerIndex, currentPlayerIndex);
    }

    public void ChangeTempo(int bpm, int beatPerBar)
    {
        UIScript.ChangeTempo(bpm, beatPerBar);
    }

    public void SetLastRiff()
    {
        _riffManager._isLastRiff = true;
    }
    
    void EndGame() 
    {
        _isGameEnded = true;
        enabled = false;
        _metronome.EndGame();
    }
}