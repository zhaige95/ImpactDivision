using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SoundMgr : MonoBehaviour {
    [Header("Sound Setting------------------")]
    public AudioMixer audioMixer;
    public SliderOption masterVolume;
    public SliderOption soundVolume;
    public SliderOption musicVolume;
    public SliderOption talkVolume;
    public bool initialized = false;
    // Use this for initialization
    public void Init()
    {
        InitSetting();
        masterVolume.OnChanged = SetMasterVolume;
        soundVolume.OnChanged = SetSoundVolume;
        musicVolume.OnChanged = SetMusicVolume;
        talkVolume.OnChanged = SetTalkVolume;
        
        audioMixer.SetFloat("MasterVolume", masterVolume.slider.value);
        audioMixer.SetFloat("SoundVolume", soundVolume.slider.value);
        audioMixer.SetFloat("MusicVolume", musicVolume.slider.value);
        audioMixer.SetFloat("TalkVolume", talkVolume.slider.value);

    }
    public void InitSetting()
    {
        var setting = Battle.systemSettingSave;
        masterVolume.Init(setting.masterVolume);
        soundVolume.Init(setting.soundVolume);
        musicVolume.Init(setting.musicVolume);
        talkVolume.Init(setting.talkVolume);

    }

    public void ApplyChanges()
    {
        var setting = Battle.systemSettingSave;
        setting.masterVolume = int.Parse(masterVolume.countText.text);
        setting.soundVolume = int.Parse(soundVolume.countText.text);
        setting.musicVolume = int.Parse(musicVolume.countText.text);
        setting.talkVolume = int.Parse(talkVolume.countText.text);
    }

    public void SetMasterVolume(float val)
    {
        audioMixer.SetFloat("MasterVolume", val);
    }
    public void SetSoundVolume(float val)
    {
        audioMixer.SetFloat("SoundVolume", val);
    }
    public void SetMusicVolume(float val)
    {
        audioMixer.SetFloat("MusicVolume", val);
    }
    public void SetTalkVolume(float val)
    {
        audioMixer.SetFloat("TalkVolume", val);
    }


}
