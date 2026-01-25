using AYellowpaper.SerializedCollections;
using Unity.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SerializedDictionary<string, AudioClip> _gameAudioClips;
    //Different dictionary for different needs
    
    //Make the scriptable for the different notes possible?

    //Change to private
    public SerializedDictionary<string, AudioSource> _gameSounds;

    private void Awake()
    {
        foreach(var sound in _gameAudioClips)
        { 
            var audioSource = gameObject.AddComponent<AudioSource>();
            _gameSounds.Add(sound.Key, audioSource);
        }
    }

    public void PlaySound(string soundName)
    {
        _gameSounds[soundName].Play();
    }
}