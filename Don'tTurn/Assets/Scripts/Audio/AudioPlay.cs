using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public void playAudio(string clipName)
    {
        AudioManager.AudioManagerInstance.Play(clipName);
    }
}
