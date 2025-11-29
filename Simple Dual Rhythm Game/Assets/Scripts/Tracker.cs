using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    PartUI partUIScript;
    [SerializeField] private GameObject recordingIcon;
    [SerializeField] private GameObject playingIcon;
    [SerializeField] private GameObject silenceIcon;

    [SerializeField] private GameObject player1Icon;
    [SerializeField] private GameObject player2Icon;

    private void Start() {
        //TODO change that
        partUIScript = GameObject.Find("Part").GetComponent<PartUI>();
        
        recordingIcon.SetActive(false);
        playingIcon.SetActive(false);
        silenceIcon.SetActive(false);
        
        player1Icon.SetActive(false);
        player2Icon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        //TODO: Modify that, super ineffective
        if (other.gameObject.name == "TrackerTrigger") {
            if(partUIScript.currentTracker == null)
                partUIScript.currentTracker = this.gameObject;
        }
    }

    public void AssignIcon(Metronome.GameState gameState, bool player1)
    {
        recordingIcon.SetActive(gameState == Metronome.GameState.Recording);
        playingIcon.SetActive(gameState == Metronome.GameState.Playing);
        silenceIcon.SetActive(gameState == Metronome.GameState.Silence);

        if (gameState != Metronome.GameState.Silence)
        {
            player1Icon.SetActive(player1);
            player2Icon.SetActive(!player1);
        }
        else
        {
            player1Icon.SetActive(false);
            player2Icon.SetActive(false);
        }
    }
}