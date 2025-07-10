using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private Metronome metronomeScript;

    private Player _currentPlayer;
    public Player CurrentPlayer => _currentPlayer;
    
    Player player1;
    Player player2;

    public Text score1;
    public Text score2;

    [SerializeField] private GameManager gameManager;
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
        gameManager.startGame.AddListener(StartGame);
    }

    void StartGame()
    {
        player1 = new Player(score1, 0);
        player2 = new Player(score2, 1);
        _currentPlayer = player1;

        switch (Parameters.instance.inputMode) {
            case InputMode.gamepad:
                GetComponent<PlayerInputManager>().playerPrefab.GetComponent<PlayerInput>().defaultActionMap = "Controller";
                break;

            case InputMode.keyboard:
                GetComponent<PlayerInputManager>().playerPrefab.GetComponent<PlayerInput>().defaultActionMap = "Arrows";
                break;
        }
    }

    public void ProcessInput(int playerIndex, int note) {
        if (playerIndex == _currentPlayer.index) {
            metronomeScript.PlayNote(note);
        }
    }

    public void changeCurrentPlayer() {
        if (_currentPlayer.Equals(player1)) {
            _currentPlayer = player2;
        }
        else {
            _currentPlayer = player1;
        }
    }

    public void MakePoints(int points) {
        _currentPlayer.points += points;
        if (_currentPlayer.points < 0) _currentPlayer.points = 0;
        _currentPlayer.scoreUI.text = ": " + _currentPlayer.points; //"Score: " + 
        Debug.Log("Points: " + _currentPlayer.points);
    }
}