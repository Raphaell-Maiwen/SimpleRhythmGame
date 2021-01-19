using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    Metronome metronomeScript;

    Player currentPlayer;
    Player player1;
    Player player2;

    public Text score1;
    public Text score2;

    GameManager gameManager;

    Controls controls;

    public InputMode inputMode;

    public enum InputMode {
        gamepad,
        keyboard
        //eventually: keytar
    }

    //TODO: A delegate to call a different function based on the control scheme?

    public class Player {
        public int points;
        public Text scoreUI;

        public int index;

        public Player(Text UI, int i) {
            scoreUI = UI;
            index = i;
        }
    }

    private void Awake() {
        controls = new Controls();
    }

    void Start(){
        gameManager = GameObject.FindObjectOfType<GameManager>();


        metronomeScript = GameObject.Find("Metronome").GetComponent<Metronome>();

        player1 = new Player(score1, 0);
        player2 = new Player(score2, 1);
        currentPlayer = player1;

        switch (inputMode) {
            case InputMode.gamepad:
                this.GetComponent<PlayerInputManager>().playerPrefab.GetComponent<PlayerInput>().defaultActionMap = "Controller";
                break;

            case InputMode.keyboard:
                this.GetComponent<PlayerInputManager>().playerPrefab.GetComponent<PlayerInput>().defaultActionMap = "Arrows";
                break;
        }
    }

    public void SetControls(int controlScheme) {
        /*controls.Arrows.Disable();
        controls.Controller.Disable();
        //keytar

        switch (controlScheme) {
            case 0:
                controls.Arrows.Enable();
                break;
            case 1:
                controls.Controller.Enable();
                break;
            case 2:
                //keytar
                break;
        }*/
    }

    public void ProcessInput(int playerIndex, int note) {
        if (playerIndex == currentPlayer.index) {
            metronomeScript.PlayNote(note);
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
    }

    public void MakePoints(int points) {
        currentPlayer.points += points;
        if (currentPlayer.points < 0) currentPlayer.points = 0;
        currentPlayer.scoreUI.text = "Score: " + currentPlayer.points;
        Debug.Log("Points: " + currentPlayer.points);
    }

    private void OnEnable() {
        /*controls.Arrows.Enable();
        controls.Gamepad.Enable();*/
    }

    private void OnDisable() {
        /*controls.Arrows.Disable();
        controls.Gamepad.Disable();*/
    }
}