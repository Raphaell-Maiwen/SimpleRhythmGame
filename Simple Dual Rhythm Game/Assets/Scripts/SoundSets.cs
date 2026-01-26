using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSets : ScriptableObject
{
    [SerializeField] private SoundSet _defaultSoundSet;
    public SoundSet DefaultSoundSet => _defaultSoundSet;

    [SerializeField] private SoundSet[] _soundSetsPool;
    public SoundSet[] SoundSetsPool => _soundSetsPool;

    [Serializable]
    public struct SoundSet
    {
        public string soundSetName;
        [SerializeField] public List<AudioClip> _audioClips;
        [SerializeField] public List<AudioClip> _finaleAudioClips;
    }
}
