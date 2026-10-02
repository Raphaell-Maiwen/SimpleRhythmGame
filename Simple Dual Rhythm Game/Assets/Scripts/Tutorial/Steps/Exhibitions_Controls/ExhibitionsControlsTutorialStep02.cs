using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionsControlsTutorialStep02 : TutorialStep
{
    public override void ProcessEvent(EventType eventType, int code)
    {

        if (eventType == EventType.FretPressed && code == 0)
        {
            _tutorialNotesList[0].SetPressed();
        }
        else if (eventType == EventType.FretReleased && code == 0)
        {
            _tutorialNotesList[0].ResetColor();
        }

        if (eventType == EventType.NotePlayed && code == 0)
        {
            OnCompleted();
        }
    }
}
