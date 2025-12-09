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

    public UnityEvent<bool> _onGamePausedOrUnpaused;

    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePauseMenu();
        }
    }

    public void ResumeGame() {
        TogglePauseMenu();
    }

    public void TogglePauseMenu() { 
        _isActive = !_isActive;

        _container.SetActive(_isActive);
        Time.timeScale = _isActive ? 0 : 1;

        //TODO: Have a centralized audio system
        _onGamePausedOrUnpaused?.Invoke(_isActive);
    }

    public void PlayAgain() {
        WindowsDeviceApiService.ListWindowsRawDeviceApiDevicesToConsole();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
