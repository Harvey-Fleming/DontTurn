using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System;

public class VideoSettings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    bool isFullscreen = true;

    Resolution[] resolutions;

    void Start()
    {
        Screen.fullScreen = isFullscreen;
        showResolutions();     
    }

    private void showResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions(); //Clears options in the dropdown
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + "@" + resolutions[i].refreshRate + "hz"; ;
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
 
    public void SetResolution (int resolutionIndex) 
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, isFullscreen, resolution.refreshRate);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log(Screen.fullScreen);
    }
}
