using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMode : GameLoop
{
    public override void PlayNote(int noteIndex, bool isCurrentPlayer)
    {
        Debug.Log("Playing in the race!");
    }
}
