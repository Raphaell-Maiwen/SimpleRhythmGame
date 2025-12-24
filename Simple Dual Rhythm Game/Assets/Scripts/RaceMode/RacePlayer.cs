using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RaceMode;

public class RacePlayer : MonoBehaviour
{
    private Queue<int> _playerNotesQueue = new Queue<int>();
    private Queue<RaceNote> _spawnedRaceNotes = new Queue<RaceNote>();
    private Transform _notesSpawnAnchor;
    private List<NestedRaceNotesList> _currentPool = new List<NestedRaceNotesList>();
    private RaceMode _raceMode;
    private Text _notesLeftText;

    private int _playerIndex;

    public void Init (Transform notesSpawnAnchor, List<NestedRaceNotesList> currentPool, RaceMode raceMode, int playerIndex, Text notesLeft) 
    {
        _notesSpawnAnchor = notesSpawnAnchor;
        _currentPool = currentPool;
        _raceMode = raceMode;
        _playerIndex = playerIndex;
        _notesLeftText = notesLeft;
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

            Vector3 notesPos = _notesSpawnAnchor.transform.position;
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

        if(_spawnedRaceNotes.Count == 0) 
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
    }

    private void UpdateNotesLeftUI()
    {
        _notesLeftText.text = "Notes lefts: " + (_playerNotesQueue.Count + _spawnedRaceNotes.Count);
    }

    private void WrongNote()
    {
        //Play sound wrong note + UI thingy
        
        for (int i = 0; i < _raceMode.GetNotePenalty(); i++)
        {
            _playerNotesQueue.Enqueue(Random.Range(0,4));
        }
    }
}
