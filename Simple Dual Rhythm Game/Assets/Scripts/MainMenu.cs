using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        WindowsDeviceApiService.ListWindowsRawDeviceApiDevicesToConsole();

        switch (Parameters.instance.inputMode)
        {
            case InputMode.keytar:
                SceneManager.LoadScene("RegisterKeyboards");
                break;
            default:
                SceneManager.LoadScene("Dual");
                break;
        }
    }

    public void QuitGame() {
        Application.Quit();
    }
}
