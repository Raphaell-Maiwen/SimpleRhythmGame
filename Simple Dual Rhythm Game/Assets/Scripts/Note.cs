using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteIcon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _playedColor;
    [SerializeField] private Color _missedColor;

    private int _index;
    private NoteState _noteState = NoteState.Unplayed;
    
    //Add Particles and/or tweens too here?

    public void ResetIcon()
    {
        _spriteRenderer.color = Color.black;
        _noteState = NoteState.Unplayed;
    }

    public void SetPlayedIcon()
    {
        _spriteRenderer.color = _playedColor;
    }

    public void SetMissedIcon()
    {
        _spriteRenderer.color = _missedColor;
    }

    public void SetIndex(int index) 
    {
        _index = index;
    }

    public int GetIndex() 
    { 
        return _index;
    }

    public void ChangeState(NoteState newState) 
    {
        _noteState = newState;

        //Switch case here with the set icon etc?
    }

    public NoteState GetState() 
    {
        return _noteState;
    }
}

public enum NoteState
{
    Unplayed,
    Played,
    Missed,
    Wrong
}
