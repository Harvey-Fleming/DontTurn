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
        List<string> options = new List<string>(); //Make a new list of options that we will place resolutions into
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) //Loops for an amount of times equal to number of potential resolutions
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + "@" + resolutions[i].refreshRate + "hz"; ; //Creates options for each potential resolution
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
 
    public void SetResolution (int resolutionIndex) //will change resolution when dropdown option is selected
    {
        Resolution resolution = resolutions[resolutionIndex]; //returns the desired resolution based on the index of dropdown selected
        Screen.SetResolution(resolution.width, resolution.height, isFullscreen, resolution.refreshRate); //sets the resolution to the desired settings
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; //Toggles fullscreen based on boolean value
        Debug.Log(Screen.fullScreen);
    }
}
