using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersManager : MonoBehaviour
{
    Dictionary<int, KeyCode> Player1Inputs;
    Dictionary<int, KeyCode> Player2Inputs;
    Dictionary<int, KeyCode> currentInputs;
    Metronome metronomeScript;

    Player currentPlayer;
    Player player1;
    Player player2;

    public Text score1;
    public Text score2;

    public class Player {
        public Dictionary<int, KeyCode> playerInputs;
        public int points;
        public Text scoreUI;

        public Player(Dictionary<int, KeyCode> inputs, Text UI) {
            playerInputs = inputs;
            scoreUI = UI;
        }
    }

    void Awake(){
        Player1Inputs = SetPlayerInputs(KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow);
        Player2Inputs = SetPlayerInputs(KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.W);
        metronomeScript = GameObject.Find("Metronome").GetComponent<Metronome>();

        player1 = new Player(Player1Inputs, score1);
        player2 = new Player(Player2Inputs, score2);
        currentPlayer = player1;
        currentInputs = player1.playerInputs;
    }

    void Update(){
        for (int i = 0; i < currentInputs.Count; i++) {
            if (Input.GetKeyDown(currentInputs[i])) {
                metronomeScript.PlayNote(i);
                break;
            }
        }
    }

    Dictionary<int, KeyCode> SetPlayerInputs(KeyCode KeyCode1, KeyCode KeyCode2, KeyCode KeyCode3, KeyCode KeyCode4) {
        Dictionary<int, KeyCode> playersInput = new Dictionary<int, KeyCode>();

        playersInput[0] = KeyCode1;
        playersInput[1] = KeyCode2;
        playersInput[2] = KeyCode3;
        playersInput[3] = KeyCode4;

        return playersInput;
    }

    public void changeCurrentPlayer() {
        if (currentPlayer.Equals(player1)) {
            currentPlayer = player2;
        }
        else {
            currentPlayer = player1;
        }
        currentInputs = currentPlayer.playerInputs;
    }

    public void MakePoints(int points) {
        currentPlayer.points += points;
        if (currentPlayer.points < 0) currentPlayer.points = 0;
        currentPlayer.scoreUI.text = "Score: " + currentPlayer.points;
        Debug.Log("Points: " + currentPlayer.points);
    }
}