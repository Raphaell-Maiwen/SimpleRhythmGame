using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TutorialConfig", menuName = "ScriptableObjects/TutorialConfig", order = 1)]
public class TutorialConfig : ScriptableObject
{
    [SerializeField] private GameObject[] _tutorialSteps;
    public GameObject[] TutorialSteps => _tutorialSteps;
    
    [SerializeField] private string _sceneToLoad;
    public string SceneToLoad => _sceneToLoad;
}
