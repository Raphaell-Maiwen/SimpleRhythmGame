using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour {
    public int bpm;

    [HideInInspector]
    public float frequency;

    public int beatPerBar;

    private int metronomeCounter = 0;

    private float initialTime;
    private float newBarTime;

    private AudioSource tickSound;
    private AudioSource[] instrumentSounds;
    private AudioSource[] sounds;

    private float points = 0;
    private float errorMargin = 0.2f;

    [Range(0, 1)]
    public float strongTick;
    [Range(0, 1)]
    public float weakTick;

    public Camera thisCamera;

    private int lastCounter = 1;

    PlayersManager playersScript;
    BarsUI UIScript;

    List<Note> riff;
    private int riffCounter = 0;

    public class Note {
        public int noteCode;
        public float time;

        public Note(int note, float timeInSec) {
            noteCode = note;
            time = timeInSec;
        }
    }

    //TODO: Maybe make a "ChangePlayer" state?? Would skip a state immediately
    public enum GameState {
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
        UIScript = GameObject.Find("Bar").GetComponent<BarsUI>();
        UIScript.SetUp(this.bpm, this.beatPerBar);
        currentStateIndex = statesSeries.Length - 1;
        initialTime = Time.time;
        riff = new List<Note>();
    }

    void tick() {
        //Check if we're at the beginning of a new bar
        if (metronomeCounter % beatPerBar == 0) {
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
            else if (currentState == GameState.Playing) {
                riffCounter = 0;
            }

            newBarTime = Time.time;
            UIScript.NewBar();
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
            UIScript.EraseAllNotes();
        }
        currentState = statesSeries[currentStateIndex];

        ChangeVisuals();
    }

    void Update() {
        float timeSpent = Time.time - initialTime;

        if (currentState == GameState.Playing && riff.Count > 0 && riffCounter < riff.Count &&
            ((Time.time - newBarTime) > riff[riffCounter].time + errorMargin)) {
            riffCounter++;
        }

        if (timeSpent >= frequency * metronomeCounter + 1) {
            tick();
        }
    }

    bool IsRightNote(int noteIndex) {
        float noteTime = Time.time - newBarTime;

        for (int i = riffCounter; i < riff.Count; i++) {
            if (Mathf.Abs(riff[i].time - noteTime) <= errorMargin) {
                if (riff[i].noteCode == noteIndex) {
                    riffCounter++;
                    Debug.Log("Good note");
                    return true;
                }
            }
            else {
                Debug.Log("Bad note");
                break;
            }
        }

        return false;
    }

    void makePoints() {
        points++;
        Debug.Log("Points: " + points);
    }

    public void PlayNote(int noteIndex) {
        if (currentState == GameState.Silence) {
            return;
        }
        else if (currentState == GameState.Recording) {
            Note newNote = new Note(noteIndex, Time.time - newBarTime);
            riff.Add(newNote);
            UIScript.DrawNewNote(noteIndex);
        }
        else if (currentState == GameState.Playing && riffCounter < riff.Count) {
            if (IsRightNote(noteIndex)) {
                //Call MakePoints in the PlayersManager script
            }
            //False tu perds des points
        }

        Debug.Log("Note " + noteIndex);
        instrumentSounds[noteIndex].Play();
    }

    void ChangeVisuals() {
        if (currentState == GameState.Recording) {
            thisCamera.backgroundColor = Color.red;
        }
        else if (currentState == GameState.Playing) {
            thisCamera.backgroundColor = Color.green;
        }
        else if (currentState == GameState.Silence) {
            thisCamera.backgroundColor = Color.blue;
        }
    }
}