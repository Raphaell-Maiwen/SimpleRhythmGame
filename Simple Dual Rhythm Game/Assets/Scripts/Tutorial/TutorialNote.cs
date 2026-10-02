using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialNote : MonoBehaviour
{
    public int value;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _pressedColor;
    [SerializeField] private Color _playedColor;
    
    [SerializeField] private float _punchStrenght;
    [SerializeField] private float _punchDuration;
    [SerializeField] private int _punchVibrato;
    
    private List<Tweener> _tweeners = new List<Tweener>();
    
    private NoteState _state;

    public void ResetColor()
    {
        _spriteRenderer.color = _normalColor;
        _state = NoteState.Normal;
    }

    public void SetPressed()
    {
        _spriteRenderer.color = _pressedColor;
        _state =  NoteState.Pressed;
    }

    public void SetPlayed()
    {
        _spriteRenderer.color = _playedColor;
        _state = NoteState.Played;
        
        _tweeners.Add(transform.DOPunchScale(new Vector3(_punchStrenght, _punchStrenght, 0), _punchDuration, _punchVibrato, 0));
    }

    //Useful??
    public enum NoteState
    {
        Normal,
        Pressed,
        Played
    };
}
