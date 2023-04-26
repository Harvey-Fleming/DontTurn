using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video; 

public class CurseTutorialPanel : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    int tutorialNumber; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (tutorialNumber)
        {
            case 1:
                videoPlayer.clip = videoClips[0];
                break;
            case 2:
                videoPlayer.clip = videoClips[1];
                break;
            case 3:
                videoPlayer.clip = videoClips[2];
                break;
            case 4:
                videoPlayer.clip = videoClips[3];
                break;
            case 5:
                videoPlayer.clip = videoClips[4];
                break;
            case 6:
                videoPlayer.clip = videoClips[5];
                break;
        }
    }
}
