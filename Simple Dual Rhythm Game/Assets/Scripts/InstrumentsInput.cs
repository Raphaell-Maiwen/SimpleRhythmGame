using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstrumentsInput : MonoBehaviour
{
    PlayerInput playerInput;

    public InputMode inputMode;

    PlayersManager playersManager;

    public enum InputMode {
        gamepad,
        keyboard
        //eventually: keytar
    }

    //bool[] FKeysPressed;

    private void Awake() {
        Debug.Log("InstrumentsInput belongs to " +gameObject.name);
        
        playerInput = GetComponent<PlayerInput>();

        //FKeysPressed = new bool[5];

        playersManager = GameObject.FindObjectOfType<PlayersManager>();

        //Making sure a device doesn't join if it's not supposed to (and doesn't prevent right devices to join)
        if (typeof(Keyboard) == playerInput.devices[0].GetType()) {
            if (playersManager.inputMode == PlayersManager.InputMode.gamepad) GameObject.Destroy(this.gameObject);
        }
        else if (typeof(Gamepad) == playerInput.devices[0].GetType()) {
            if (playersManager.inputMode == PlayersManager.InputMode.keyboard) GameObject.Destroy(this.gameObject);
        }
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
