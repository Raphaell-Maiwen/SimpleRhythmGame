using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMode : GameLoop
{
    [SerializeField] private Parameters _parameters;

    public List<int> _notes = new List<int>();
    public List<int> _player1StackNotes = new List<int>();
    public List<int> _player2StackNotes = new List<int>();

    public void Start()
    {
        for (int i = 0; i < _parameters.numberOfNotes; i++)
        {
            //Stack differently depending on input mode
            int newNote = Random.Range(0, 4);
            _notes.Add(newNote);
            _player1StackNotes.Add(newNote);
            _player2StackNotes.Add(newNote);
        }
    }

    public override void PlayNote(int noteIndex, int playerIndex, int currentPlayerIndex)
    {
        Debug.Log("Playing in the race!");

        List<int> playerStack = playerIndex == 0 ? _player1StackNotes : _player2StackNotes;

        if (noteIndex == playerStack[playerStack.Count - 1])
        {
            Debug.Log("Right note");
            playerStack.RemoveAt(playerStack.Count - 1);
        }
        else 
        {
            //Add notes to stack
            Debug.Log("Wrong note");
        }
    }

    //Function to spawn block of notes
}
