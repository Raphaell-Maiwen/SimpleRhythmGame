using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private PartUI _partUIScript;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject recordingIcon;
    [SerializeField] private GameObject playingIcon;
    [SerializeField] private GameObject silenceIcon;

    [SerializeField] private GameObject player1Icon;
    [SerializeField] private GameObject player2Icon;

    private void Start() {
        recordingIcon.SetActive(false);
        playingIcon.SetActive(false);
        silenceIcon.SetActive(false);
        
        player1Icon.SetActive(false);
        player2Icon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if(1 << other.gameObject.layer == _layerMask.value)
        {    //Double check the usefulness of that check
            if(_partUIScript.currentTracker == null)
                _partUIScript.currentTracker = this.gameObject;
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