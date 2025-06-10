using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmEngine : MonoBehaviour
{
    delegate void CurrentFunction(int device, int key, int pressed);
    CurrentFunction current;
    //static class? delegates? something?

    static List<int> PlayersKeyboards;
    static GamePhase currentPhase = GamePhase.Registering;

    static int CurrentPlayer;

    InputRedirector IR;

    public enum GamePhase { 
        Registering,
        Playing
    }

    private void Start()
    {
        PlayersKeyboards = new List<int>();
        //current = RegisterKeyboard;
    }

    public static void ProcessInput(int device, int key, bool pressed) {
        switch (currentPhase) {
            case GamePhase.Playing:
                PlayingNote(device, key, pressed);
                break;
            case GamePhase.Registering:
                RegisterKeyboard(device, key, pressed);
                break;
        }
    }

    public static void RegisterKeyboard(int device, int key, bool pressed) {
        if (pressed) {
            //TODO: Convert to dictionary???
            if (PlayersKeyboards.Count == 1 && PlayersKeyboards[0] == device)
            {
                Debug.Log("Device already registered.");
                return;
            }
            PlayersKeyboards.Add(device);
            Debug.Log("Adding device " + device);
            if (PlayersKeyboards.Count == 2)
            {
                Debug.Log("All devices registered.");
                currentPhase = GamePhase.Playing;
                return;
            }
            CurrentPlayer = device;
        }
    }

    public static void PlayingNote(int device, int key, bool pressed)
    {
        if (pressed) {
            if (device == CurrentPlayer)
            {
                Debug.Log("Oui");
            }
            else
            {
                Debug.Log("Non");
            }
        }
    }

    void NullFunction(int device, int key, bool pressed) {
        Debug.Log("Doing nothing");
        return;
    }
}
