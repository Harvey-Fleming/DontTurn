using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioManagerInstance {get; private set;}

    [SerializeField] private AudioMixerGroup MainMixerGroup;
    [SerializeField] private AudioMixerGroup SFXMixerGroup;
    [SerializeField] private AudioMixerGroup MusicMixerGroup;

    [SerializeField] private Sound[] sounds; 

    private void Awake() 
    {
        DontDestroyOnLoad(this);

        //Checks to see if there is already an instance of the audiomanager in the scene.
        if (AudioManagerInstance != null && AudioManagerInstance != this)
        {
            Destroy(this.gameObject);
            return;
        } 
        else
        {
            AudioManagerInstance = this; //if no other instance of AudioManager, this object is the instance
        }
        

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;

            switch (s.audiotype)
            {
                case Sound.AudioTypes.soundEffect:
                s.source.outputAudioMixerGroup = SFXMixerGroup;
                break;

                case Sound.AudioTypes.music:
                s.source.outputAudioMixerGroup = MusicMixerGroup;
                break;
            }
        }

        
    }

    //Searches the sound array for a clipName with exact match to play and returns error if no sound file found.
    public void Play(string clipName)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipName);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipName + " Not Found");
            return;
        }
        s.source.Play();  
    }

    //Searches for if a specific clip is playing and stops it.
    public void Stop(string clipName)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipName);
        if (s == null)
        {
            Debug.Log("Sound: " + clipName + " Not Found");
            return;
        }
        s.source.Stop();  
    }

    //Updates volume mixers with value from sliders in options menu.
    public void UpdateMixerVolume()
    {
        MusicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(AudioOptionsManager.musicVolume) * 20);
        SFXMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(AudioOptionsManager.soundEffectsVolume) * 20);
        MainMixerGroup.audioMixer.SetFloat("MasterVolume", Mathf.Log10(AudioOptionsManager.mainVolume) * 20);
    }


}
