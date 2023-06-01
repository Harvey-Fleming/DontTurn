using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFadeEvent : MonoBehaviour
{
    public FollowCamera followCam;
    
    public void FollowCamEvent()
    {
        followCam.SetCameraPos();
    }
}
