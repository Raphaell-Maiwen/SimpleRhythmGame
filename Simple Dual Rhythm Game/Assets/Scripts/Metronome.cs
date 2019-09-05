using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour {
    /*
     2 1
     3 2 
     4 3
     5 4

        6 5
        7 6
        8 7
        9 8

        10 9 
        11 10
        12 11
        13 12
         
         */

    public int bpm;

    [HideInInspector]
    public float frequency;

    private int counter = 0;

    private float initialTime;

    private AudioSource tickSound;
    private AudioSource[] instrumentSounds;
    private AudioSource[] sounds;

    private float points = 0;
    private float errorMargin = 0.2f;

    private bool bipAllowed = true;
    private int lastCounter = 1;

    PlayersManager playersScript;

    void Start() {
        frequency = 60f / bpm;

        sounds = GetComponents<AudioSource>();
        tickSound = sounds[0];

        instrumentSounds = new AudioSource[4];

        for (int i = 0; i < 4; i++) {
            instrumentSounds[i] = sounds[i + 1];
        }

        playersScript = GameObject.Find("PlayersManager").GetComponent<PlayersManager>();
        initialTime = Time.time;
    }

    void tick() {
        tickSound.Play();

        if (counter != 0 && counter % 4 == 0) {
            playersScript.changeCurrentPlayer();
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
        if (isOnTime()) {
            makePoints();
        }

        Debug.Log("Note " + noteIndex);
        instrumentSounds[noteIndex].Play();
    }
}