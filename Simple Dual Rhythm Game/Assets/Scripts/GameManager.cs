using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    public int soloIndex;
    public int solosToDo;
    Metronome metronomeScript;

    public void AddSolo() {
        soloIndex++;
        if (soloIndex == solosToDo)
            LastSolo();
    }

    private void Awake() {
        metronomeScript = GameObject.Find("Metronome").GetComponent<Metronome>();
    }

    void LastSolo() {
       /* metronomeScript.bpm *= 2;
        metronomeScript.SetTempo();*/
        //Change visuals?
    }
}