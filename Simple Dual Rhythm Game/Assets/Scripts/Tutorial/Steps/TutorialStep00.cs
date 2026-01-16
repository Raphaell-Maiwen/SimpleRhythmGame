using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStep00 : TutorialStep
{
    private int something = 0;
    

    public override void ProcessEvent(EventType eventType, int code)
    {
        Debug.Log("Reached me!");
        something++;

        if(something == 3) 
        {
            OnCompleted();
        }
    }
}
