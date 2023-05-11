using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureManager : MonoBehaviour
{
    public int cureAmount = 0;
    public int allCures;

    public void IncreaseCureCount()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.ItemPickup, this.transform.position);
        cureAmount++; 
    }
}
