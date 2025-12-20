using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMode : GameLoop
{
    [SerializeField] private Parameters _parameters;
    [SerializeField] private int _notesPenalty;
    [SerializeField] private int _blockSize;
    [SerializeField] private float _noteSize;

    [SerializeField] private List<NestedRaceNotesList> _arrowsPool;
    [SerializeField] private List<NestedRaceNotesList> _FKeysPool;
    private List<NestedRaceNotesList> _currentPool = new List<NestedRaceNotesList>();

    [SerializeField] private Transform _notesSpawnAnchor;
    [SerializeField] private Transform _outOfScreenAnchor;
    private Queue<RaceNote> _spawnedRaceNotes = new Queue<RaceNote>();

    //Maybe Queue?
    public List<int> _notes = new List<int>();
    public List<int> _player1StackNotes = new List<int>();
    public List<int> _player2StackNotes = new List<int>();

    public void Start()
    {
        if (_parameters.inputMode == InputMode.keyboard)
        {
            _currentPool = _arrowsPool;
        }
        else if (_parameters.inputMode == InputMode.keytar)
        {
            _currentPool = _FKeysPool;
        }

        for (int i = 0; i < _parameters.numberOfNotes; i++)
        {
            //Stack differently depending on input mode
            int newNote = Random.Range(0, 4);
            _notes.Add(newNote);
            _player1StackNotes.Add(newNote);
            _player2StackNotes.Add(newNote);
        }
        SpawnBlock();
    }

    public override void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex)
    {
        List<int> playerStack = playerIndex == 0 ? _player1StackNotes : _player2StackNotes;

        if (noteIndex == playerStack[playerStack.Count - 1])
        {
            GoodNote(playerStack);
        }
        else 
        {
            WrongNote(playerStack);
        }
    }

    //Function to spawn block of notes
    private void SpawnBlock() 
    {
        //*2, one per player
        //Check how many notes left
        for (int i = 0; i < _blockSize; i++) 
        {
            var notesList = _currentPool[_player1StackNotes[_player1StackNotes.Count - 1 - i]].raceNotes;
            var note = notesList[0];

            Vector3 notesPos = _notesSpawnAnchor.transform.position;
            notesPos.y += i * _noteSize;

            note.transform.position = notesPos;
            _spawnedRaceNotes.Enqueue(note);
            notesList.RemoveAt(0);
        }
    }

    private void GoodNote(List<int> playerStack) 
    {
        playerStack.RemoveAt(playerStack.Count - 1);
        var note = _spawnedRaceNotes.Dequeue();

        note.transform.position = _outOfScreenAnchor.transform.position;
    }

    private void WrongNote(List<int> playerStack) 
    {
        //Play sound wrong note + UI thingy
        for (int i = 0; i < _notesPenalty; i++) 
        {
            playerStack.Insert(0, Random.Range(0, 4));
        }
    }

    [System.Serializable]
    public class NestedRaceNotesList
    {
        public List<RaceNote> raceNotes;
    }
}
