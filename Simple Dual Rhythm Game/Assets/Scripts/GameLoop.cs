using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameLoop: MonoBehaviour
{
    //public void ProcessInput(int playerIndex, int note);
    public abstract void PlayNote(int noteIndex);
}
