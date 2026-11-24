using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MetronomeEventChannel", menuName = "ScriptableObjects/EventChannel", order = 1)]
public class MetronomeEventChannel : ScriptableObject
{
    private Metronome _metronome;
    public Metronome Metronome => _metronome;
    
    public void RegisterMetronome(Metronome metronome)
    {
        Debug.Log("RegisterMetronome");
        _metronome = metronome;
    }

    public void OnStartTutorial(TutorialStepsManager stepsManager)
    {
        _metronome.TutorialStartGame(stepsManager);
    }
}