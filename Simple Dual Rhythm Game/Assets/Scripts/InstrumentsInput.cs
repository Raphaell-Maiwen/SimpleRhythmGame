using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class InstrumentsInput : MonoBehaviour
{
    PlayerInput playerInput;
    PlayersManager playersManager;

    public InputMode inputMode;
    
    //<DeviceID, PlayerID>
    private Dictionary<int, int> deviceMapping = new Dictionary<int, int>();

    //<Player ID, <Note ID, PressedOrNot>>
    Dictionary<int, Dictionary<int, bool>> keytarChord;

    bool registeringKeyboards;

    private void Awake()
    {
        inputMode = Parameters.instance.inputMode;
        
        //TODO: Branch depending on the inputMode
        if (inputMode == InputMode.keytar)
        {
            registeringKeyboards = true;
        }

        keytarChord = new Dictionary<int, Dictionary<int, bool>>()
        {
            {
                0, new Dictionary<int, bool>()
                {
                    {0, false},
                    {1, false},
                    {2, false},
                    {3, false}
                }
            },
            {
                1, new Dictionary<int, bool>()
                {
                    {0, false},
                    {1, false},
                    {2, false},
                    {3, false}
                }
            }
        };
        
        playerInput = GetComponent<PlayerInput>();
        playersManager = GameObject.FindObjectOfType<PlayersManager>();

        //Making sure a device doesn't join if it's not supposed to (and doesn't prevent right devices to join)
        if (typeof(Keyboard) == playerInput.devices[0].GetType()) {
            if (Parameters.instance.inputMode == InputMode.gamepad) GameObject.Destroy(this.gameObject);
        }
        else if (typeof(Gamepad) == playerInput.devices[0].GetType()) {
            if (Parameters.instance.inputMode == InputMode.keyboard) GameObject.Destroy(this.gameObject);
        }
        
        Debug.Log("Player Input Devices 0" + playerInput.devices[0]);
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
        //How to only process what we wanna process

        if (registeringKeyboards)
        {
            RegisterKeyboard(device);
            return;
        }

        //If we press enter
        if (pressed && key == 13)
        {
            foreach (var player in keytarChord.Keys)
            {
                foreach (var note in keytarChord[player].Keys)
                {
                    if (keytarChord[player][note])
                    {
                        playersManager.ProcessInput(player, note);
                    }
                }
            }
        }
        
        //TODO: Expand to 116
        else if (key > 111 && key < 116)
        {
            keytarChord[0][key % 112] = pressed;
        }
    }

    private void RegisterKeyboard(int device)
    {
        if (!deviceMapping.ContainsKey(device))
        {
            deviceMapping.Add(device, deviceMapping.Count);
        }

        if (deviceMapping.Count == 2)
        {
            registeringKeyboards = false;
            GameObject.FindObjectOfType<GameManager>().startGame?.Invoke();
        }
    }
}













