using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameLoop: MonoBehaviour
{
    //public void ProcessInput(int playerIndex, int note);
    [SerializeField] protected PauseMenu _pauseMenu;
    public PauseMenu PauseMenu => _pauseMenu;
    public abstract void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex);
}
