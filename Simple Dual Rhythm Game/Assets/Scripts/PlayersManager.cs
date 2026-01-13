using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerJoinText;
    [SerializeField] private Parameters _parameters;
    [SerializeField] private GameLoop _gameLoopScript;
    private List<InstrumentsInput> _instrumentsInputs;

    [SerializeField] private Color _player1Color;
    [SerializeField] private Color _player2Color;

    private Player _currentPlayer;
    public Player CurrentPlayer => _currentPlayer;

    public UnityEvent OnCurrentPlayerChanged;
    
    Player player1;
    Player player2;

    public Text score1;
    public Text score2;

    [SerializeField] private GameManager _gameManager;
    Controls controls;

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
        _gameManager.startGame.AddListener(StartGame);
        
        if (_parameters.inputMode == InputMode.keytar)
        {
            ShowPressInputToJoin(1);
        }
    }

    void StartGame()
    {
        player1 = new Player(score1, 0);
        player2 = new Player(score2, 1);
        _currentPlayer = player1;
        OnCurrentPlayerChanged?.Invoke();

        switch (_parameters.inputMode) {
            case InputMode.gamepad:
                PlayerInputManager.instance.playerPrefab.GetComponent<PlayerInput>().defaultActionMap = "Controller";
                break;

            case InputMode.keyboard:
                PlayerInputManager.instance.playerPrefab.GetComponent<PlayerInput>().defaultActionMap = "Arrows";
                break;
        }
    }

    public void InitInstrumentsInput(PlayerInput input)
    {
        var instrumentsInput = input.GetComponent<InstrumentsInput>();
        instrumentsInput.Init(this, _gameLoopScript.PauseMenu);
        _gameLoopScript.PauseMenu._onGamePausedOrUnpaused.AddListener(instrumentsInput.TogglePause);
    }

    public void ProcessInput(int playerIndex, int note) {
        _gameLoopScript.PlayNote(note, playerIndex, _currentPlayer.index);

        if (playerIndex == -1 && note == -1) { 
            ((Metronome)_gameLoopScript).OnRPressed();
        }
    }

    public void changeCurrentPlayer() {
        if (_currentPlayer.Equals(player1)) {
            _currentPlayer = player2;
        }
        else {
            _currentPlayer = player1;
        }
        OnCurrentPlayerChanged?.Invoke();
    }
    

    public void MakePoints(int points) {
        _currentPlayer.points += points;
        if (_currentPlayer.points < 0) _currentPlayer.points = 0;
        _currentPlayer.scoreUI.text = ": " + _currentPlayer.points;
    }

    public int DeclareWinner()
    {
        return player1.points > player2.points ? 1 : 2;
    }

    public void ShowPressInputToJoin(int player)
    {
        if (player == 0) _playerJoinText.text = "";
        else _playerJoinText.text = "Player " + player + ": Press on a key to register keyboard";
    }
}