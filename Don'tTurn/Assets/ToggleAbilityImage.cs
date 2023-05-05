using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAbilityImage : MonoBehaviour
{
    public Sprite shotgun;
    public Sprite grapple;
    public Sprite question;
    private Image iconHolder;
    public PrototypeDash dashScript;
    public GrappleAbility grappleScript;

    private void Start()
    {
        iconHolder = GetComponent<Image>();

        iconHolder.sprite = question;
    }

    public void SwitchToShotgun()
    {
        if (dashScript.isUnlocked)
        {
            iconHolder.sprite = shotgun;
        }
        else
        {
            iconHolder.sprite = question;
        }
    }

    public void SwitchToGrapple()
    {
        if (grappleScript.isUnlocked)
        {
            iconHolder.sprite = grapple;
        }
        else
        {
            iconHolder.sprite = question;
        }
    }
}
