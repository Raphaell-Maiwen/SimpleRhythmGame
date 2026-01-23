using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;
using static RaceMode;

public class RacePlayer : MonoBehaviour
{
    private Queue<int> _playerNotesQueue = new Queue<int>();

    private Queue<RaceNote> _spawnedRaceNotes = new Queue<RaceNote>();
    //TODO: Modify this
    private Transform _notesTarget;
    private List<NestedRaceNotesList> _currentPool = new List<NestedRaceNotesList>();
    private RaceMode _raceMode;
    private Text _notesLeftText;

    private int _playerIndex;

    private Coroutine _moveNotesDownCoroutine;
    private float _moveDownSpeed;

    public void Init(Transform notesSpawnAnchor, List<NestedRaceNotesList> currentPool, RaceMode raceMode, int playerIndex, Text notesLeft, float moveDownSpeed) 
    {
        _notesTarget = notesSpawnAnchor;
        _currentPool = currentPool;
        _raceMode = raceMode;
        _playerIndex = playerIndex;
        _notesLeftText = notesLeft;
        _moveDownSpeed = moveDownSpeed;
    }

    private void Start()
    {
        UpdateNotesLeftUI();
    }

    public void SpawnBlock() 
    {
        //Check how many notes left
        for (int i = 0; i < _raceMode.GetBlockSize() && _playerNotesQueue.Count != 0; i++)
        {
            var index = _playerNotesQueue.Dequeue();
            var notesList = _currentPool[index].raceNotes;
            var note = notesList[0];

            note.value = index;

            Vector3 notesPos = _notesTarget.transform.position;
            notesPos.y += i * _raceMode.GetNoteSize();

            note.transform.position = notesPos;
            _spawnedRaceNotes.Enqueue(note);
            notesList.RemoveAt(0);
        }
    }

    public void AddToStackNotes(int newNote) 
    { 
        _playerNotesQueue.Enqueue(newNote);
    }

    public void PlayNote(int noteIndex, int currentPlayerIndex)
    {
        if (noteIndex == _spawnedRaceNotes.Peek().value)
        {
            GoodNote();

            if (_spawnedRaceNotes.Count == 0)
            {
                CheckEndOfRaceOrBlock();
            }
            else if (_moveNotesDownCoroutine == null)
            {
                _moveNotesDownCoroutine = StartCoroutine("MoveNotesDown");
            }
        }
        else 
        {
            WrongNote();
        }

        UpdateNotesLeftUI();
    }

    private void GoodNote()
    {
        var note = _spawnedRaceNotes.Dequeue();

        note.transform.position = _raceMode._outOfScreenAnchor.transform.position;
        _currentPool[note.value].raceNotes.Add(note);
        _raceMode.PlaySound(note.value);
    }

    private void CheckEndOfRaceOrBlock()
    {
        if (_playerNotesQueue.Count == 0)
        {
            _raceMode.EndOfRace(_playerIndex);
        }
        else
        {
            SpawnBlock();
        }
    }

    private void UpdateNotesLeftUI()
    {
        _notesLeftText.text = "Notes lefts: " + (_playerNotesQueue.Count + _spawnedRaceNotes.Count);
    }

    private void WrongNote()
    {
        //Do something in the UI
        _raceMode.PlaySound(-1);
        
        for (int i = 0; i < _raceMode.GetNotePenalty(); i++)
        {
            _playerNotesQueue.Enqueue(Random.Range(0,4));
        }
    }

    private IEnumerator MoveNotesDown()
    {
        while (_spawnedRaceNotes.Peek().transform.position.y - _notesTarget.position.y > 0.1f)
        {
            MoveNotes();
            yield return null;
        }

        MoveNotes();
        _moveNotesDownCoroutine = null;
        yield return null;
    }

    private void MoveNotes()
    {
        var currentNoteY = _notesTarget.position.y;
        int counter = 0;

        //Vector3 direction = _notesTarget.position - note.transform.position;
        //note.transform.position += (direction.normalized * _moveDownSpeed * Time.deltaTime);

        foreach (var note in _spawnedRaceNotes)
        {
            float targetHeight = currentNoteY + counter * _raceMode.GetNoteSize();
            Vector3 target = new Vector3(_notesTarget.position.x, targetHeight, _notesTarget.position.z);

            //normalized?
            Vector3 direction = (target - note.transform.position);

            note.transform.position += (direction.normalized * _moveDownSpeed * Time.deltaTime);

            counter++;
        }
    }
}
