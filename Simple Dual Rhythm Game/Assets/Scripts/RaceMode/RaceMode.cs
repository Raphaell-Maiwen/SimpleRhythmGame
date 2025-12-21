using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayersManager;

public class RaceMode : GameLoop
{
    [SerializeField] private Parameters _parameters;
    [SerializeField] private int _notesPenalty;
    [SerializeField] private int _notesPerBlock;
    [SerializeField] private float _noteSize;

    [SerializeField] private List<NestedRaceNotesList> _arrowsPool;
    [SerializeField] private List<NestedRaceNotesList> _FKeysPool;
    private List<NestedRaceNotesList> _currentPool = new List<NestedRaceNotesList>();

    [SerializeField] private Transform _notesSpawnAnchorOnePlayer;
    [SerializeField] private Transform _notesSpawnAnchorTwoPlayers1;
    [SerializeField] private Transform _notesSpawnAnchorTwoPlayers2;
    [SerializeField] public Transform _outOfScreenAnchor;

    private List<RacePlayer> _players = new List<RacePlayer>();

    public Queue<int> _notes = new Queue<int>();

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

        if (_parameters.numberOfPlayers == 1)
        {
            CreatePlayer(_notesSpawnAnchorOnePlayer);
        }
        else 
        {
            CreatePlayer(_notesSpawnAnchorTwoPlayers1);
            CreatePlayer(_notesSpawnAnchorTwoPlayers2);
        }

        for (int i = 0; i < _parameters.numberOfNotes; i++)
        {
            //Stack a different range depending on input mode
            int newNote = Random.Range(0, 4);
            _notes.Enqueue(newNote);

            foreach(RacePlayer player in _players)
            {
                player.AddToStackNotes(newNote);
            }
        }

        foreach (RacePlayer player in _players)
        {
            player.SpawnBlock();
        }
    }

    private void CreatePlayer(Transform anchor) 
    {
        RacePlayer player = gameObject.AddComponent<RacePlayer>();
        player.Init(anchor, _currentPool, this);
        _players.Add(player);
    }

    public override void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex)
    {
        if (playerIndex < _players.Count)
        {
            _players[playerIndex].PlayNote(noteIndex, currentPlayerIndex);
        }
    }

        public void EndOfRace() 
    {
        if (_parameters.numberOfPlayers == 1)
        {
            CheckHighestScores();
        }
        else 
        { 
            
        }
    }

    private void CheckHighestScores() 
    { 
        //Compare depending on modes
    }

    public int GetBlockSize() 
    {
        return _notesPerBlock;
    }

    public float GetNoteSize() 
    {
        return _noteSize;
    }

    public int GetNotePenalty()
    {
        return _notesPenalty;
    }

    [System.Serializable]
    public class NestedRaceNotesList
    {
        public List<RaceNote> raceNotes;
    }
}
