using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    Dictionary<int, InputAction> Player1Inputs;
    Dictionary<int, InputAction> Player2Inputs;
    Dictionary<int, InputAction> currentInputs;
    Metronome metronomeScript;

    Player currentPlayer;
    Player player1;
    Player player2;

    public Text score1;
    public Text score2;

    GameManager gameManager;

    Controls controls;

    //TODO: A delegate to call a different function based on the control scheme?

    public class Player {
        public Dictionary<int, InputAction> playerInputs;
        public int points;
        public Text scoreUI;

        public Player(Dictionary<int, InputAction> inputs, Text UI) {
            playerInputs = inputs;
            scoreUI = UI;
        }
    }

    private void Awake() {
        controls = new Controls();
    }

    void Start(){
        gameManager = GameObject.FindObjectOfType<GameManager>();

        switch (gameManager.controlsSettings) {
            case GameManager.ControlsSettings.arrows:
                Player1Inputs = SetPlayerInputs(controls.Arrows.Down, controls.Arrows.Left,
                    controls.Arrows.Right, controls.Arrows.Up);

                Player2Inputs = SetPlayerInputs(controls.Arrows.S, controls.Arrows.A,
                    controls.Arrows.D, controls.Arrows.W);
                
                break;
            case GameManager.ControlsSettings.controller:
                Player1Inputs = SetPlayerInputs(controls.Gamepad.A, controls.Gamepad.X, controls.Gamepad.B, controls.Gamepad.Y);
                Player2Inputs = SetPlayerInputs(controls.Gamepad.A, controls.Gamepad.X, controls.Gamepad.B, controls.Gamepad.Y);
                break;
            case GameManager.ControlsSettings.keytar:
                //Todo something for the keytar
                break;
        }

        metronomeScript = GameObject.Find("Metronome").GetComponent<Metronome>();

        player1 = new Player(Player1Inputs, score1);
        player2 = new Player(Player2Inputs, score2);
        currentPlayer = player1;
        currentInputs = player1.playerInputs;
    }

    void Update(){
        for (int i = 0; i < currentInputs.Count; i++) {
            //print(currentInputs[0]);
            //print(Input.GetKeyDown(currentInputs[0]));

            if (currentInputs[i].triggered) {
                metronomeScript.PlayNote(i);
                break;
            }
        }
    }

    //Send a list or array of InputAction
    Dictionary<int, InputAction> SetPlayerInputs(InputAction Action1, InputAction Action2, InputAction Action3, InputAction Action4) {
        Dictionary<int, InputAction> playersInput = new Dictionary<int, InputAction>();

        playersInput[0] = Action1;
        playersInput[1] = Action2;
        playersInput[2] = Action3;
        playersInput[3] = Action4;

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

    private void OnEnable() {
        //Faire un switch pis tout'
        controls.Arrows.Enable();
        controls.Gamepad.Enable();
    }

    private void OnDisable() {
        controls.Arrows.Disable();
        controls.Gamepad.Disable();
    }
}