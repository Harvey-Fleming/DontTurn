using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtons : MonoBehaviour
{
    [SerializeField] private AudioManager AudioManagerInstance;
    
    private void Start() 
    {
        AudioManagerInstance = AudioManager.AudioManagerInstance;
    }
    
    public void onClick(string clipName)
    {
        AudioManagerInstance.Play(clipName);
    }

}
