using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionsControlsTutorialStep04 : TutorialStep
{
    private int notesPressedInARow = 0;
    //Add a "don't release it!" message

    public override void ProcessEvent(EventType eventType, int code)
    {
        if (eventType == EventType.FretReleased)
        {
            notesPressedInARow = 0;
            Debug.Log("Streak broken");
            //Showcase "don't release fret!" message
        }
        else if (eventType == EventType.NotePlayed && code == 0)
        {
            notesPressedInARow++;
        }
        
        Debug.Log(notesPressedInARow);

        if(notesPressedInARow == 3) 
        {
            OnCompleted();
        }
    }
}
