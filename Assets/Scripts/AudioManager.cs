using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] private Track[] _sounds = null;
    [SerializeField] private Track[] _musics = null;
    [SerializeField] private List<string> _musicNamesGameplay = null;

    private string currentMusicName;
    
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Track s in _sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = s.volume;
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Track m in _musics)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.volume = m.volume;
            m.source.clip = m.clip;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
        }
    }

    public void PlaySound(string name)
    {
        Track s = Array.Find(_sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        
        s.source.Play();
    }

    public void PlayMusic(string name)
    {
        Track m = Array.Find(_musics, music => music.name == name);
        if (m == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }

        currentMusicName = name;
        m.source.Play();
    }

    public void PlayRandomMusic()
    {
        var rnd = Random.Range(0, _musicNamesGameplay.Count - 1);
        var nameMusic = _musicNamesGameplay[rnd];
        
        PlayMusic(nameMusic);
    }

    public void StopMusic(string name)
    {
        Track m = Array.Find(_musics, music => music.name == name);
        if (m == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }

        m.source.Stop();
    }

    public void StopCurrentMusic()
    {
        StopMusic(currentMusicName);
    }
    
    public void StopSound(string name)
    {
        Track s = Array.Find(_sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }
}