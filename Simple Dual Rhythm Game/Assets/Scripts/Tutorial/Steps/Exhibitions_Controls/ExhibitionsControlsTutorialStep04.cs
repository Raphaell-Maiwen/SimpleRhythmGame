using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhibitionsControlsTutorialStep04 : TutorialStep
{
    private int notesPressedInARow = 0;
    //reset notesPressedInARow

    public override void ProcessEvent(EventType eventType, int code)
    {
        if (eventType == EventType.FretReleased && code == 0)
        {
            notesPressedInARow = 0;
            ShowErrorMessage("dontRelease");

            foreach (var note in _tutorialNotesList)
            {
                note.ResetColor();
            }
        }
        else if (eventType == EventType.FretPressed && code == 0)
        {
            _tutorialNotesList[notesPressedInARow].SetPressed();
            HideErrorMessage();
        }
        else if (eventType == EventType.NotePlayed && code == 0)
        {
            _tutorialNotesList[notesPressedInARow].SetPlayed();
            notesPressedInARow++;
        }

        if(notesPressedInARow == 3) 
        {
            OnCompleted();
        }
    }
}
