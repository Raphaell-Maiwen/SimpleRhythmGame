using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteIcon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _playedColor;
    [SerializeField] private Color _missedColor;
    
    //Add Particles and/or tweens too here?

    public void SetPlayedIcon()
    {
        _spriteRenderer.color = _playedColor;
    }

    public void SetMissedIcon()
    {
        _spriteRenderer.color = _missedColor;
    }
}
