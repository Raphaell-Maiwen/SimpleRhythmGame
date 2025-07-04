using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    PartUI partUIScript;
    [SerializeField] private GameObject recordingIcon;
    [SerializeField] private GameObject playingIcon;
    [SerializeField] private GameObject silenceIcon;

    private void Start() {
        //TODO change that
        partUIScript = GameObject.Find("Part").GetComponent<PartUI>();
        
        recordingIcon.SetActive(false);
        playingIcon.SetActive(false);
        silenceIcon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "TrackerTrigger") {
            if(partUIScript.currentTracker == null)
                partUIScript.currentTracker = this.gameObject;
        }
    }

    public void AssignIcon(Metronome.GameState gameState)
    {
        recordingIcon.SetActive(gameState == Metronome.GameState.Recording);
        playingIcon.SetActive(gameState == Metronome.GameState.Playing);
        silenceIcon.SetActive(gameState == Metronome.GameState.Silence);
    }
}