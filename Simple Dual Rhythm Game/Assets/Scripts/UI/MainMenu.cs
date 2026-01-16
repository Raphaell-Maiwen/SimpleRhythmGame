using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        WindowsDeviceApiService.ListWindowsRawDeviceApiDevicesToConsole();

        SceneManager.LoadScene("Dual");
    }

    public void PlayRace()
    {
        SceneManager.LoadScene("Race");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
