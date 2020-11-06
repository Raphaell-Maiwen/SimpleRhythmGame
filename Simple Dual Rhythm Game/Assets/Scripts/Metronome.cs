using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    private AudioSource beat;

    private float errorMargin = 0.2f;
    private int notesSucceeded;

    [Range(0, 1)]
    public float strongTick;
    [Range(0, 1)]
    public float weakTick;

    public Camera thisCamera;

    private int lastCounter = 1;

    PlayersManager playersScript;
    PartUI UIScript;

    List<Note> riff;
    private int riffCounter = 0;

    bool firstTick = true;
    bool madeMistake = false;

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
        

        sounds = GetComponents<AudioSource>();
        tickSound = sounds[0];

        instrumentSounds = new AudioSource[4];

        for (int i = 0; i < 4; i++) {
            instrumentSounds[i] = sounds[i + 1];
        }

        playersScript = GameObject.Find("PlayersManager").GetComponent<PlayersManager>();
        UIScript = GameObject.Find("Part").GetComponent<PartUI>();

        SetTempo();

        currentStateIndex = statesSeries.Length - 1;
        riff = new List<Note>();

        beat = sounds[sounds.Length - 1];

        //beat.Play();
    }

    private void Start() {
        AudioMixerGroup pitchBendGroup = Resources.Load<AudioMixerGroup>("MyAudioMixer");
        beat.outputAudioMixerGroup = pitchBendGroup;
        float speed = bpm / 60f;
        beat.pitch = speed;
        pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / speed);
    }

    public void SetTempo() {
        frequency = 60f / bpm;
        UIScript.SetUp(this.bpm, this.beatPerBar);
        initialTime = Time.time;
        metronomeCounter = 0;
    }

    void tick() {
        if (firstTick) {
            firstTick = false;
            beat.Play();
        }

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

            //Change this code slightly when supporting multiple bars
            notesSucceeded = 0;
            madeMistake = false;
        }
        else {
            tickSound.volume = weakTick;
        }
        //TODO: Reset counter at 0 instead??
        metronomeCounter++;
        //tickSound.Play();
    }

    void ChangeState() {
        currentStateIndex++;
        if (currentStateIndex == statesSeries.Length) {
            currentStateIndex = 0;
            UIScript.EraseAllNotes();
            GameObject.FindObjectOfType<GameManager>().AddSolo();
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
                notesSucceeded++;
                int points = notesSucceeded * 10;
                playersScript.MakePoints(points);

                //Bonus points for a perfect solo
                if (notesSucceeded == riff.Count && !madeMistake) {
                    //Should be 200 for composer and 400 for the other, will change at some point
                    playersScript.MakePoints(400);
                }
            }
            else {
                madeMistake = true;
                int penalty = ((riff.Count * (riff.Count + 1)) / 2 * 10) / riff.Count;
                playersScript.MakePoints(-penalty);
            }
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