using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TutorialConfigChannel _tutorialConfigChannel;
    [SerializeField] private TutorialConfig _tutorialConfig;
    
    public void PlayGame()
    {
        WindowsDeviceApiService.ListWindowsRawDeviceApiDevicesToConsole();

        SceneManager.LoadScene("Dual");
    }

    public void PlayRace()
    {
        SceneManager.LoadScene("Race");
    }

    public void StartControlsTutorial()
    {
        _tutorialConfigChannel.SetConfig(_tutorialConfig);
        SceneManager.LoadScene("ControlsTutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
