using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour {
    public int bpm;

    [HideInInspector]
    public float frequency;

    private int counter = 0;

    private float initialTime;
    private float newBarTime;

    private AudioSource tickSound;
    private AudioSource[] instrumentSounds;
    private AudioSource[] sounds;

    private float points = 0;
    private float errorMargin = 0.2f;

    private bool bipAllowed = true;
    private int lastCounter = 1;

    PlayersManager playersScript;

    List<Dictionary<float, int>> riff;

    public enum GameState{
        Recording,
        Playing,
        Silence
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
    }

    void tick() {
        tickSound.Play();

        //TODO: Can be something else than 4
        //Check if we're at the beginning of a new bar
        if (counter % 4 == 0) {
            //Change state of the game
            currentStateIndex++;
            if (currentStateIndex == statesSeries.Length) {
                currentStateIndex = 0;
            }
            currentState = statesSeries[currentStateIndex];

            //Reset the riff if we're recording again, change player if it's a silence...

            playersScript.changeCurrentPlayer();
            newBarTime = Time.time;
        }

        counter++;
    }

    void Update() {
        float timeSpent = Time.time - initialTime;
        if (timeSpent >= frequency * (counter) + errorMargin) {
            bipAllowed = true;
        }

        if (timeSpent >= frequency * counter + 1) {
            tick();
        }
    }

    bool isOnTime() {
        float currentTime = Time.time - initialTime;
        float validTime = frequency * counter;
        //Debug.Log ("Guitar:" + currentTime + " " + validTime);
        if (bipAllowed && (currentTime <= frequency * (counter - 1) + errorMargin)) {
            lastCounter = counter;
            bipAllowed = false;
            return true;
        }
        else if (bipAllowed && currentTime >= validTime - errorMargin) {
            lastCounter = counter + 1;
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
        else {
            /*if (isOnTime()) {
            makePoints();
            }*/
        }

        Debug.Log("Note " + noteIndex);
        instrumentSounds[noteIndex].Play();
    }
}