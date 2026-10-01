using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialStepData", menuName = "ScriptableObjects/TutorialStepData", order = 1)]
public class TutorialStepData : ScriptableObject
{
    //NEXT step: add list of objects to spawn, position and scale
    //Add dictionary of error messages
    
    [SerializeField] private string _instructions;
    public string Instructions => _instructions;

    [SerializeField] private Vector2 _panelPos;
    public Vector2 PanelPos => _panelPos;

    [SerializeField] private bool _pressAnyKeyToContinueWindow;
    public bool PressAnyKeyToContinueWindow => _pressAnyKeyToContinueWindow;

    [SerializeField] private List<TutorialObjectToSpawn> _objectToSpawns;
    public  List<TutorialObjectToSpawn> ObjectToSpawns => _objectToSpawns;
}

[Serializable]
public class TutorialObjectToSpawn
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector2 _tutorialObjectPos;
    [SerializeField] private float _scale;
    
    public GameObject Prefab => _prefab;
    public Vector2 TutorialObjectPos => _tutorialObjectPos;
    public  float Scale => _scale;
}
