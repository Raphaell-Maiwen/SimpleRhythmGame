using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using static SoundSets;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Parameters _parameters;

    public SerializedDictionary<string, AudioClip> _gameAudioClips;
    //Different dictionary for different needs

    //Expand the beats when I'll be there
    //Default beat variable

    public SerializedDictionary<string, AudioSource> _gameSounds = new SerializedDictionary<string, AudioSource>();

    private AudioSource _beat;

    private void Awake()
    {
        foreach(var sound in _gameAudioClips)
        {
            AddSound(sound.Key, sound.Value);
        }

        SetUpNotes(_parameters.player1SoundsSet, 0);
        SetUpNotes(_parameters.player2SoundsSet, 1);
    }

    private void SetUpNotes(string playerSoundsSet, int playerInt)
    {
        var pool = _parameters.SoundSetsSO.SoundSetsPool;

        if (pool.Any(s => s.soundSetName == playerSoundsSet))
        {
            var player1SoundSet = pool.First(s => s.soundSetName == playerSoundsSet);

            AddSoundsSet(player1SoundSet._audioClips, playerInt);
        }
        else
        {
            if (!pool.Any(s => s.soundSetName.Contains("Default")))
            {
                Debug.LogError("Sound set with that name doesn't exist.");
            }

            AddSoundsSet(_parameters.SoundSetsSO.DefaultSoundSet._audioClips, playerInt);
        }
    }

    private void AddSoundsSet(List<AudioClip> audioClips, int playerInt)
    {
        for (int i = 0; i < audioClips.Count; i++)
        {
            AddSound("player_" + playerInt + "_" + i, audioClips[i]);
        }
    }

    private void AddSound(string soundName, AudioClip audioClip)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = audioClip;
        _gameSounds.Add(soundName, audioSource);
    }

    public void PlayBeat()
    {
        _beat.Play();
    }

    public void PlayNote(int player, int noteCode)
    {
        PlaySound("player_" + player + "_" + noteCode);
    }

    public void PlaySound(string soundName)
    {
        if (_gameSounds.ContainsKey(soundName))
        {
            _gameSounds[soundName].Play();
        }
        else
        {
            Debug.LogError("Sound name doesn't exist");
        }
    }

    public void SetBeat(string beatName)
    {
        _beat = _gameSounds[beatName];
        _beat.loop = true;
    }

    public void SetBeatSpeed(int bpm)
    {
        AudioMixerGroup pitchBendGroup = Resources.Load<AudioMixerGroup>("MyAudioMixer");
        _beat.outputAudioMixerGroup = pitchBendGroup;
        float speed = bpm / 60f;
        _beat.pitch = speed;
        pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / speed);
    }

    public void PauseBeat()
    {
        _beat.Pause();
    }

    public void UnPauseBeat()
    {
        _beat.UnPause();
    }
}