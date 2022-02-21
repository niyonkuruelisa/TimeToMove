using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider musicVolumeSlider;
    public Slider gameVolumeSlider;

    public AudioMixerGroup audioMixerGroup;

    void Start()
    {
        musicVolumeSlider.onValueChanged.AddListener(delegate { MusicValueChangeCheck(); });
        gameVolumeSlider.onValueChanged.AddListener(delegate { GameValueChangeCheck(); });
    }

    public void MusicValueChangeCheck()
    {
        //audioMixerGroup.audioMixer.FindMatchingGroups("musicVol")[0].audioMixer.SetFloat("Music", musicVolumeSlider.value);
        audioMixerGroup.audioMixer.SetFloat("musicVol", musicVolumeSlider.value);
    }

    public void GameValueChangeCheck()
    {
        audioMixerGroup.audioMixer.SetFloat("masterVol", gameVolumeSlider.value);
    }

}
