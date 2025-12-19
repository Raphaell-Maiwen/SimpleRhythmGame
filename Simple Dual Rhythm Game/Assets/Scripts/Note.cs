using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoteIcon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _playedColor;
    [SerializeField] private Color _missedColor;

    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;
    [SerializeField] private int _shakeVibrato;

    private int _index;
    private NoteState _noteState = NoteState.Unplayed;

    //Add Particles and/or tweens too here?

    private void Start()
    {
        DOTween.Init(this);
    }

    public void ResetIcon()
    {
        _spriteRenderer.color = Color.black;
    }

    public void SetPlayedIcon()
    {
        _spriteRenderer.color = _playedColor;
    }

    public void SetMissedIcon()
    {
        _spriteRenderer.color = _missedColor;
    }

    public void SetWrongIcon() 
    {
        _spriteRenderer.color = _missedColor;
        transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato);
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

        switch (newState) 
        { 
            case NoteState.Unplayed:
                ResetIcon();
                break;
            case NoteState.Played:
                SetPlayedIcon();
                break;
            case NoteState.Missed: 
                SetMissedIcon();
                break;
            case NoteState.Wrong:
                SetWrongIcon();
                break;
        } 
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
