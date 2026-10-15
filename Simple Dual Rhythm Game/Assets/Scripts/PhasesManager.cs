using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PhasesManager: MonoBehaviour
{
    [ReadOnly] public static List<GameState> statesSeries = new List<GameState>();
    
    public static GameState CurrentState = GameState.Playing;
    public static GameState NextState = GameState.Playing;
    public static  int CurrentStateIndex;
    public static int NextStateIndex;

    public static Action OnAllPhasesCycledThrough;

    public void Init(List<GameState> states, Metronome metronome)
    {
        statesSeries = states;
        
        CurrentStateIndex = statesSeries.Count - 1;
        NextStateIndex = 0;
        
        //Assign metronome?
    }
    
    public static void ChangeState(int newStateIndex)
    {
        CurrentStateIndex = newStateIndex;

        if (CurrentStateIndex == statesSeries.Count)
        {
            CurrentStateIndex = 0;
            //REgister event for tutorial here
            OnAllPhasesCycledThrough?.Invoke();
        }

        CurrentState = statesSeries[CurrentStateIndex];
    }
    
    public static void ChangeNextState(int newStateIndex)
    {
        NextStateIndex = newStateIndex;
        
        if (NextStateIndex == statesSeries.Count)
        {
            NextStateIndex = 0;
        }
        
        NextState = statesSeries[NextStateIndex];
        
        if (NextState == GameState.ChangePlayer)
        {
            NextStateIndex++;
            NextState = statesSeries[NextStateIndex];
        }
    }
    
    public static GameState GetPreviousState(int newStateIndex)
    {
        int previousStateIndex = newStateIndex;
        previousStateIndex--;

        if (previousStateIndex < 0)
        {
            previousStateIndex = statesSeries.Count - 1;
        }
        
        return  statesSeries[previousStateIndex];
    }
}

public enum GameState
{
    Recording,
    Playing,
    Silence,
    ChangePlayer
};