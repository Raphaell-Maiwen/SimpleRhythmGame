using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PhasesManager: MonoBehaviour
{
    [ReadOnly] public List<GameState> statesSeries = new List<GameState>();
    
    public GameState CurrentState = GameState.Playing;
    public GameState NextState = GameState.Playing;
    public  int CurrentStateIndex;
    public int NextStateIndex;

    public void Init(List<GameState> states, Metronome metronome)
    {
        statesSeries = states;
        
        CurrentStateIndex = statesSeries.Count - 1;
        NextStateIndex = 0;
        
        //Assign metronome
    }
}

public enum GameState
{
    Recording,
    Playing,
    Silence,
    ChangePlayer
};