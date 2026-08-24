using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialConfig", menuName = "ScriptableObjects/TutorialConfig", order = 1)]
public class TutorialConfig : ScriptableObject
{
    [SerializeField] private TutorialStep[] tutorialSteps;
    public  TutorialStep[] TutorialSteps => tutorialSteps;
}
