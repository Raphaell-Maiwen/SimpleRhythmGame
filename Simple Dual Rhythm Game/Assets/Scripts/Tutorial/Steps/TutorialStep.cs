using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialStep : MonoBehaviour
{
    [SerializeField] private TutorialStepData _stepData;
    public TutorialStepData StepData => _stepData;
    protected TutorialStepsManager _stepsManager;

    protected List<TutorialNote> _tutorialNotesList = new List<TutorialNote>();

    public void Init(TutorialStepsManager tutorialStepsManager)
    { 
        _stepsManager = tutorialStepsManager;
    }

    public abstract void ProcessEvent(EventType eventType, int code = -1);
    public void OnCompleted()
    {
        _stepsManager.IncrementStep();
    }

    private void Start()
    {
        foreach (var obj in _stepData.ObjectToSpawns)
        {
            var spawnedObject = Instantiate(obj.Prefab);
            spawnedObject.transform.position = obj.TutorialObjectPos;
            spawnedObject.transform.localScale = new Vector3(obj.Scale, obj.Scale, obj.Scale);
            
            spawnedObject.transform.parent = transform;

            spawnedObject.TryGetComponent(out TutorialNote note);

            if (note != null)
            {
                _tutorialNotesList.Add(note);
            }
        }
    }
}
