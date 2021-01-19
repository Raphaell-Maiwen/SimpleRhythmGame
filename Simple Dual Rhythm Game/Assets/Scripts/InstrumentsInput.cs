using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstrumentsInput : MonoBehaviour
{
    PlayerInput playerInput;
    //Controls controls;

    public InputMode inputMode;

    PlayersManager playersManager;

    public enum InputMode {
        gamepad,
        keyboard
        //eventually: keytar
    }

    //bool[] FKeysPressed;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();

        //controls = new Controls();

        //FKeysPressed = new bool[5];

        playersManager = GameObject.FindObjectOfType<PlayersManager>();

        /*switch (inputMode) {
            case InputMode.gamepad:
                playerInput.defaultActionMap = "Controller";
                break;
            case InputMode.keyboard:
                playerInput.defaultActionMap = "Arrows";
                break;*/
                /*case InputMode.keytar:
                    playerInput.defaultActionMap = "PlayerKeytar";
                    break;*/
        //}
    }

    void Start(){
        /*switch (inputMode) {
            case InputMode.gamepad:
                playerInput.defaultActionMap = "Controller";
                break;
            case InputMode.keyboard:
                playerInput.defaultActionMap = "Arrows";
                break;*/
            /*case InputMode.keytar:
                playerInput.defaultActionMap = "PlayerKeytar";
                break;*/
        //}
    }


    //Gamepad Inputs

    private void OnSouth() {
        print("Ok");
        playersManager.ProcessInput(playerInput.playerIndex, 0);
    }

    private void OnWest() {
        playersManager.ProcessInput(playerInput.playerIndex, 1);
    }

    private void OnEast() {
        playersManager.ProcessInput(playerInput.playerIndex, 2);
    }

    private void OnNorth() {
        playersManager.ProcessInput(playerInput.playerIndex, 3);
    }

    //Player 1 Keyboard Inputs

    private void OnS() {
        playersManager.ProcessInput(0, 0);
    }

    private void OnA() {
        playersManager.ProcessInput(0, 1);
    }

    private void OnD() {
        playersManager.ProcessInput(0, 2);
    }

    private void OnW() {
        playersManager.ProcessInput(0, 3);
    }

    //Player 2 Keyboard Inputs
    private void OnDown() {
        playersManager.ProcessInput(1, 0);
    }

    private void OnLeft() {
        playersManager.ProcessInput(1, 1);
    }

    private void OnRight() {
        playersManager.ProcessInput(1, 2);
    }

    private void OnUp() {
        playersManager.ProcessInput(1, 3);
    }
}
