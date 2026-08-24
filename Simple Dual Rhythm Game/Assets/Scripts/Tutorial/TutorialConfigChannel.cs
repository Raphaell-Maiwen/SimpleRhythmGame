using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialConfigChannel : ScriptableObject
{
    private TutorialConfig _tutorialConfig;

    public void SetConfig(TutorialConfig tutorialConfig)
    {
        _tutorialConfig = tutorialConfig;
    }
    
    public  TutorialConfig GetConfig()
    {
        return _tutorialConfig;
    }
}
