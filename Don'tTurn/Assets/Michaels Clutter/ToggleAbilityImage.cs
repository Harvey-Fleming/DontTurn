using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAbilityImage : MonoBehaviour
{
    public Sprite shotgun;
    public Sprite grapple;
    public Sprite question;
    public Image iconHolder;
    public PrototypeDash dashScript;
    public GrappleAbility grappleScript;
    public GameObject toggleBG;
    public GameObject toggleBGKey;
    public PlayerStats playerStats;

    private void Start()
    {
        iconHolder.sprite = question;
        if(playerStats.abilAmount == 0)
        {
            toggleBG.SetActive(false);
            toggleBGKey.SetActive(false);
        }
    }

    public void ChangeGraphic()
    {
        if(playerStats.abilAmount == 1)
        {
            toggleBG.SetActive(true);
            toggleBGKey.SetActive(false);
        }
        if(playerStats.abilAmount == 2)
        {
            toggleBG.SetActive(true);
            toggleBGKey.SetActive(true);
        }
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
