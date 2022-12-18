using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioOptionsManager : MonoBehaviour
{
    public static float mainVolume {get; private set; }
    public static float musicVolume {get; private set; }
    public static float soundEffectsVolume {get; private set; }

    [SerializeField] private TMP_Text mainSliderText;
    [SerializeField] private TMP_Text musicSliderText;
    [SerializeField] private TMP_Text soundEffectsSliderText;


    public void OnMainSliderValueChange(float value)
    {
        mainVolume = value;

        mainSliderText.text = ((int)(value*100)).ToString();
        AudioManager.AudioManagerInstance.UpdateMixerVolume();
    }

    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;

        musicSliderText.text = ((int)(value*100)).ToString();
        AudioManager.AudioManagerInstance.UpdateMixerVolume();
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;

        soundEffectsSliderText.text = ((int)(value*100)).ToString();
        AudioManager.AudioManagerInstance.UpdateMixerVolume();
    }

}
