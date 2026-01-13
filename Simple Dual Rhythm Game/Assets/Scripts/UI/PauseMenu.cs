using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    bool _isActive = false;
    bool _isPaused = false;

    public UnityEvent<bool> _onGamePausedOrUnpaused;

    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePauseMenuUI();
        }
    }

    public void ResumeGame()
    {
        TogglePauseMenuUI();
    }

    public void TogglePauseMenuUI()
    {
        if (!_isActive && _isPaused) return;
        _isActive = !_isActive;

        _container.SetActive(_isActive);

        TogglePauseMenuBehaviour();
    }

    public void TogglePauseMenuBehaviour()
    {
        _isPaused = !_isPaused;

        Time.timeScale = _isPaused ? 0 : 1;

        //TODO: Have a centralized audio system
        _onGamePausedOrUnpaused?.Invoke(_isPaused);
    }

    public void PlayAgain()
    {
        WindowsDeviceApiService.ListWindowsRawDeviceApiDevicesToConsole();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
