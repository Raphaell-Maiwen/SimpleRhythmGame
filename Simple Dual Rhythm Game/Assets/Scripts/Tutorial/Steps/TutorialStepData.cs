using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialStepData", menuName = "ScriptableObjects/TutorialStepData", order = 1)]
public class TutorialStepData : ScriptableObject
{
    [SerializeField] private string _instructions;
    public string Instructions => _instructions;

    [SerializeField] private Vector2 _panelPos;
    public Vector2 PanelPos => _panelPos;

    [SerializeField] private bool _pressAnyKeyToContinueWindow;
    public bool PressAnyKeyToContinueWindow => _pressAnyKeyToContinueWindow;
}
