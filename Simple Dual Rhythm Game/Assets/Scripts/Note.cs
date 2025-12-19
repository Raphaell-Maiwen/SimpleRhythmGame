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

    [SerializeField] private float _punchStrenght;
    [SerializeField] private float _punchDuration;
    [SerializeField] private int _punchVibrato;

    [SerializeField] private float _fadeColorDuration;
    [SerializeField] private float _drownLength;

    private int _index;
    private Vector3 _originalPos;
    NoteState _noteState = NoteState.Unplayed;

    private List<Tweener> _tweeners = new List<Tweener>();

    //Add particles?

    private void Start()
    {
        DOTween.Init(this);
    }

    public void Init(int index, Vector3 originalPos) 
    { 
        _index = index;
        _originalPos = originalPos;

        //Tween just recorded
    }

    public void SetPooled()
    {
        ResetIcon();
    }

    public void SetUnplayed()
    {
        ResetIcon();
        transform.position = _originalPos;
        //TODO: Reset scale too
    }

    private void ResetIcon()
    {
        _spriteRenderer.color = Color.black;
        if (_tweeners.Count > 0)
        {
            foreach (var tweener in _tweeners) 
            {
                tweener.Kill();
            }
            _tweeners.Clear();
        }
    }

    public void SetPlayedIcon()
    {
        _spriteRenderer.color = _playedColor;
        _tweeners.Add(transform.DOPunchScale(new Vector3(_punchStrenght, _punchStrenght, 0), _punchDuration, _punchVibrato, 0));
    }

    public void SetMissedIcon()
    {
        _tweeners.Add(_spriteRenderer.DOColor(_missedColor, _fadeColorDuration));
        _tweeners.Add(transform.DOMoveY(transform.position.y - _drownLength, _fadeColorDuration));
    }

    public void SetWrongIcon() 
    {
        _spriteRenderer.color = _missedColor;
        _tweeners.Add(transform.DOShakePosition(_shakeDuration, _shakeStrength, _shakeVibrato));
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
            case NoteState.Pooled:
                SetPooled();
                break;
            case NoteState.Unplayed:
                SetUnplayed();
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
    Pooled,
    Unplayed,
    Played,
    Missed,
    Wrong
}
