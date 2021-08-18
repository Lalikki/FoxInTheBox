using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager: MonoBehaviour
{
    //Sound Array
    public Sound[] sounds;
    
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
    }
    //Play gametheme at start
    void Start()
    {
        Play("GameTheme");
    }
    //Playmethod for Sound Array
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        return;
        s.source.Play();
    }
}
