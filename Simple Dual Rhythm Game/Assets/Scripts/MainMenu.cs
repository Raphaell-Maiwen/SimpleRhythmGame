﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        WindowsDeviceApiService.ListWindowsRawDeviceApiDevicesToConsole();

        SceneManager.LoadScene("Dual");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
