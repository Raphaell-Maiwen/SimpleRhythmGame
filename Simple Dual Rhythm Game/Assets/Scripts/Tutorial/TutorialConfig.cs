using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialConfig", menuName = "ScriptableObjects/TutorialConfig", order = 1)]
public class TutorialConfig : ScriptableObject
{
    [SerializeField] private GameObject[] tutorialSteps;
    public GameObject[] TutorialSteps => tutorialSteps;
}
