using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InstrumentsInput : MonoBehaviour
{
    PlayerInput playerInput;

    public InputMode inputMode;

    PlayersManager playersManager;
    
    
    //<Player ID, <Note ID, PressedOrNot>>
    Dictionary<int, KeyValuePair<int, bool>> keytarChord;

    public enum InputMode {
        gamepad,
        keyboard,
        keytar
    }

    //bool[] FKeysPressed;

    private void Awake()
    {
        keytarChord = new Dictionary<int, bool>()
        {
            {0, false},
            {1, false},
            {2, false},
            {3, false}
        };
        
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
    /*private void OnS() {
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
    }*/

    public void ProcessKeytarInput(int device, int key, bool pressed)
    {
        //If we press enter
        if (pressed && key == 13)
        {
            foreach (var note in keytarChord)
            {
                if (note.Value)
                {
                    playersManager.ProcessInput(0, note.Key);
                }
            }
        }
        //TODO: Expand to 116
        else if (key > 111 && key < 116)
        {
            keytarChord[key % 112] = pressed;
        }
    }
}













