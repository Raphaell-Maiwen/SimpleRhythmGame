using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Search;

[CreateAssetMenu(fileName = "TutorialStepData", menuName = "ScriptableObjects/TutorialStepData", order = 1)]
public class TutorialStepData : ScriptableObject
{
    [SerializeField] private string _instructions;
    public string Instructions => _instructions;

    [SerializeField] private Vector2 _panelPos;
    public Vector2 PanelPos => _panelPos;

    [SerializeField] private Vector2 _errorMsgPos;
    public Vector2 ErrorMsgPos => _errorMsgPos;
    
    public SerializedDictionary<string, string> _errorMessagesDictionary;

    [SerializeField] private bool _pressAnyKeyToContinueWindow;
    public bool PressAnyKeyToContinueWindow => _pressAnyKeyToContinueWindow;

    [SerializeField] private List<TutorialObjectToSpawn> _objectToSpawns;
    public  List<TutorialObjectToSpawn> ObjectToSpawns => _objectToSpawns;
    
    [SerializeField] private List<TutorialObjectToSpawn> _persistentObjectsToSpawn;
    public  List<TutorialObjectToSpawn> PersistentObjectsToSpawn => _persistentObjectsToSpawn;
}

[Serializable]
public class TutorialObjectToSpawn
{
    [SearchContext("t:prefab")] [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector2 _tutorialObjectPos;
    [SerializeField] private float _scale;
    
    public GameObject Prefab => _prefab;
    public Vector2 TutorialObjectPos => _tutorialObjectPos;
    public  float Scale => _scale;
}
