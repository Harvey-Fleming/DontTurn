using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSeenManager : MonoBehaviour
{
    public bool hasSeenIntro;
    
    private void Awake() 
    {
        DontDestroyOnLoad(this);
    }
}
