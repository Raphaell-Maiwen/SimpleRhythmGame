using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour {
    public int bpm;

    [HideInInspector]
    public float frequency;

    private int metronomeCounter = 0;

    private float initialTime;
    private float newBarTime;

    private AudioSource tickSound;
    private AudioSource[] instrumentSounds;
    private AudioSource[] sounds;

    private float points = 0;
    private float errorMargin = 0.2f;

    [Range(0,1)]
    public float strongTick;
    [Range(0, 1)]
    public float weakTick;

    private int lastCounter = 1;

    PlayersManager playersScript;

    List<Dictionary<float, int>> riff;
    private int riffCounter = 0;

    //TODO: Maybe make a "ChangePlayer" state?? Would skip a state immediately
    public enum GameState{
        Recording,
        Playing,
        Silence,
        ChangePlayer
    };

    GameState currentState = GameState.Playing;

    public GameState[] statesSeries;
    int currentStateIndex;

    void Awake() {
        frequency = 60f / bpm;

        sounds = GetComponents<AudioSource>();
        tickSound = sounds[0];

        instrumentSounds = new AudioSource[4];

        for (int i = 0; i < 4; i++) {
            instrumentSounds[i] = sounds[i + 1];
        }

        playersScript = GameObject.Find("PlayersManager").GetComponent<PlayersManager>();
        currentStateIndex = statesSeries.Length - 1;
        initialTime = Time.time;
        riff = new List<Dictionary<float, int>>();
    }

    void tick() {
        //TODO: Can be something else than 4
        //Check if we're at the beginning of a new bar
        if (metronomeCounter % 4 == 0) {
            tickSound.volume = strongTick;
            ChangeState();
            //Reset the riff if we're recording again, change player if it's a silence...
            //TODO: How to take the error margin into account?
            if (currentState == GameState.ChangePlayer) {
                playersScript.changeCurrentPlayer();
                ChangeState();
            }
            if (currentState == GameState.Recording) {
                riff.Clear();
            }

            newBarTime = Time.time;
            Debug.Log(currentState.ToString());
        }
        else {
            tickSound.volume = weakTick;
        }
        //TODO: Reset counter at 0 instead??
        metronomeCounter++;
        tickSound.Play();
    }

    void ChangeState() {
        currentStateIndex++;
        if (currentStateIndex == statesSeries.Length) {
            currentStateIndex = 0;
        }
        currentState = statesSeries[currentStateIndex];
    }

    void Update() {
        float timeSpent = Time.time - initialTime;

        if (timeSpent >= frequency * metronomeCounter + 1) {
            tick();
        }
    }

    bool isOnTime() {
        float currentTime = Time.time - initialTime;
        float validTime = frequency * metronomeCounter;
        //Debug.Log ("Guitar:" + currentTime + " " + validTime);

        if ((currentTime <= frequency * (metronomeCounter - 1) + errorMargin)) {
            lastCounter = metronomeCounter;
            return true;
        }
        else if (currentTime >= validTime - errorMargin) {
            lastCounter = metronomeCounter + 1;
        }
        return false;
    }

    void makePoints() {
        points++;
        Debug.Log("Points: " + points);
    }

    public void PlayNote(int noteIndex) {
        if (currentState == GameState.Silence){
            return;
        }
        else if (currentState == GameState.Recording){
            Dictionary<float, int> newNote = new Dictionary<float, int>();
            newNote[Time.time - newBarTime] = noteIndex;
            riff.Add(newNote);
        }
        else if (currentState == GameState.Playing) {
            /*if (isOnTime()) {
            makePoints();
            }*/
        }

        Debug.Log("Note " + noteIndex);
        instrumentSounds[noteIndex].Play();
    }
}