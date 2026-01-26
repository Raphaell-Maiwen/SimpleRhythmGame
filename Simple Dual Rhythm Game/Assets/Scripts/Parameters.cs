using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters: ScriptableObject
{
    public int bpm;
    public int beatPerBar;
    public int bars;

    public InputMode inputMode;
    public bool fKeysOn;

    //Instruments sounds
    [SerializeField] private SoundSets _soundSetsSO;
    public SoundSets SoundSetsSO => _soundSetsSO;
    public string player1SoundsSet;
    public string player2SoundsSet;

    public int _player1SoundSetValue;
    public int _player2SoundSetValue;

    //For Race Mode
    //Enum short medium long?
    public int numberOfNotes;
    public int numberOfPlayers;

    public void SetPlayerSoundSet(int player, int value)
    {
        //Or find name? So it's less prone to errors?
        //Or populate it with the names?
        if (player == 0)
        {
            player1SoundsSet = value == 0 ? _soundSetsSO.DefaultSoundSet.soundSetName : _soundSetsSO.SoundSetsPool[value-1].soundSetName;
            _player1SoundSetValue = value;
        }
        else
        {
            player2SoundsSet = value == 0 ? _soundSetsSO.DefaultSoundSet.soundSetName : _soundSetsSO.SoundSetsPool[value - 1].soundSetName;
            _player2SoundSetValue = value;
        }
    }
}

public enum InputMode 
{
    gamepad,
    keyboard,
    keytar
}