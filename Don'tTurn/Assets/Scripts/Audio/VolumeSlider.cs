using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        Master,
        Ambience,
        SFX
    }

   [Header("Type")]
   [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;

    private void Awake()
    {

        volumeSlider = this.GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                volumeSlider.value = AudioManager.instance.masterVolume;
                break;
            case VolumeType.Ambience:
                volumeSlider.value = AudioManager.instance.AmbienceVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.SFXVolume;
                break;
            default:
                Debug.LogWarning("Volume Type not Supported" + volumeType);
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                AudioManager.instance.masterVolume = volumeSlider.value;
                break;
            case VolumeType.Ambience:
                AudioManager.instance.AmbienceVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.instance.SFXVolume = volumeSlider.value;
                break;
            default:
                Debug.LogWarning("Volume Type not Supported" + volumeType);
                break;
        }
    }

    public void OnButtonClick()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.menuTransition, this.transform.position);
    }

    public void SFXTest()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.shotgunFire, this.transform.position);
    }
}
