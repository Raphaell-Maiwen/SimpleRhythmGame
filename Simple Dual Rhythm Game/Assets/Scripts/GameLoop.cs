using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameLoop
{
    //public void ProcessInput(int playerIndex, int note);
    public void PlayNote(int noteIndex);
}
