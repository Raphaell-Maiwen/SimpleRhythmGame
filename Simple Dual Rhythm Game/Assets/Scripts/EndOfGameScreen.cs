using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _winnerText;

    public void Init(int winner)
    {
        _winnerText.text = "Player " + winner + " wins";
    }

    public void Replay()
    {
        SceneManager.LoadScene("Dual");
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
