using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    public int soloIndex;
    public int solosToDo;
    Metronome metronomeScript;

    public void AddSolo() {
        soloIndex++;
        if (soloIndex == solosToDo - 1)
            LastSolo();
        else if (soloIndex == solosToDo)
            EndOfGame();
    }

    private void Awake() {
        metronomeScript = GameObject.Find("Metronome").GetComponent<Metronome>();

        //So that each player plays the required amount of solos
        solosToDo *= 2;
    }

    void LastSolo() {
        metronomeScript.bpm *= 2;
        metronomeScript.ChangeTempo();
        //Change visuals?
    }

    void EndOfGame() {
        Time.timeScale = 0f;

        AudioSource[] sounds = GameObject.FindObjectsOfType<AudioSource>();

        foreach (AudioSource sound in sounds) {
            sound.Stop();
        }

        print("end");
    }
}