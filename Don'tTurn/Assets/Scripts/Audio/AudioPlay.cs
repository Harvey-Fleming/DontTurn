using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour, IDataPersistence
{
    public int buttonClickedCount;

    public void playAudio(string clipName)
    {
        AudioManager.AudioManagerInstance.Play(clipName);
    }

    public void onButtonClick()
    {
        buttonClickedCount++;
    }

    public void LoadData(GameData data)
    {
        this.buttonClickedCount = data.buttonclicked;
    }

    public void SaveData(GameData data)
    {
        data.buttonclicked = this.buttonClickedCount;
    }
}
