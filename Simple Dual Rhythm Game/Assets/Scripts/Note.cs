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
    private Vector3 _originalPos;
    NoteState _noteState = NoteState.Unplayed;

    private Tweener _tweener;

    //Add particles?

    private void Start()
    {
        DOTween.Init(this);
    }

    public void Init(int index, Vector3 originalPos) 
    { 
        _index = index;
        _originalPos = originalPos;
    }

    public void ResetIcon()
    {
        //Set back to original pos here?
        _spriteRenderer.color = Color.black;
        if (_tweener != null)
        { 
            _tweener.Kill();
            _tweener = null;
        }
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
        _tweener = transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato);
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
                //hmmmmmm
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
