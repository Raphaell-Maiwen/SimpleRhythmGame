using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class InstrumentsInput : MonoBehaviour
{
    [SerializeField] private Parameters _parameters;
    PlayerInput playerInput;
    PlayersManager playersManager;

    private InputMode inputMode;
    
    //<DeviceID, PlayerID>
    private Dictionary<int, int> deviceMapping = new Dictionary<int, int>();

    //<Player ID, <Note ID, PressedOrNot>>
    Dictionary<int, Dictionary<int, bool>> keytarChord;

    bool registeringKeyboards;

    private void Awake()
    {
        inputMode = _parameters.inputMode;
        playerInput = GetComponent<PlayerInput>();
        playersManager = FindObjectOfType<PlayersManager>();
        
        //Making sure a device doesn't join if it's not supposed to (and doesn't prevent right devices to join)
        if (inputMode == InputMode.keytar)
        {
            SetUpKeytars();
        }
        else
        {
            Debug.Log(playerInput.devices[0].GetType().ToString());
            
            if (inputMode == InputMode.gamepad && typeof(Keyboard) == playerInput.devices[0].GetType())
            {
                Destroy(gameObject);
            }
            else if (inputMode == InputMode.keyboard && typeof(Gamepad) == playerInput.devices[0].GetType())
            {
                Destroy(gameObject);
            }
            
            SetUpPlayerInput();
        }
    }

    private void SetUpKeytars()
    {
        registeringKeyboards = true;
            
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
        
        gameObject.AddComponent<InputRedirector>();
    }

    private void SetUpPlayerInput()
    {
        if (inputMode == InputMode.keyboard) playerInput.SwitchCurrentActionMap("Arrows");
        else playerInput.SwitchCurrentActionMap("Controller");
    }

    //Gamepad Inputs
    private void OnSouth() {
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

    private void OnR() {
        playersManager.ProcessInput(-1,-1);
    }

    public void ProcessKeytarInput(int device, int key, bool pressed)
    {
        //How to only process what we wanna process

        if (registeringKeyboards)
        {
            RegisterKeyboard(device);
            return;
        }

        int player = deviceMapping[device];

        //If we press enter
        if (pressed && key == 13)
        {
            foreach (var note in keytarChord[player].Keys)
            {
                if (keytarChord[player][note])
                {
                    playersManager.ProcessInput(player, note);
                }
            }
        }

        //TODO: Expand to 116
        else if (key > 111 && key < 116)
        {
            keytarChord[player][key % 112] = pressed;
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
            playersManager.ShowPressInputToJoin(0);
        }
        else
        {
            playersManager.ShowPressInputToJoin(2);
        }
    }
}













