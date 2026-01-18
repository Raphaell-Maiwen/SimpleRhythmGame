using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected Parameters _parameters;
    [SerializeField] protected EndOfGameScreen _endOfGameScreen;
    [SerializeField] protected PlayersManager _playersManager;
    [SerializeField] protected InstrumentsInput _instrumentsInputPrefab;
    public UnityEvent startGame;
    public UnityEvent stopGame;

    protected void Awake()
    {
        if (_parameters.inputMode == InputMode.keytar || _parameters.inputMode == InputMode.keyboard)
        {
            Instantiate(_instrumentsInputPrefab);
        }
    }

    protected void Start()
    {
        //Something with the pause menu here
        if (_parameters.inputMode != InputMode.keytar)
        {
            startGame?.Invoke();
        }
    }

    protected void EndOfGame()
    {
        stopGame?.Invoke();
        Time.timeScale = 0f;

        AudioSource[] sounds = GameObject.FindObjectsOfType<AudioSource>();

        foreach (AudioSource sound in sounds)
        {
            sound.Stop();
        }

        _endOfGameScreen.Init(_playersManager.DeclareWinner());
        _endOfGameScreen.gameObject.SetActive(true);
    }
}
