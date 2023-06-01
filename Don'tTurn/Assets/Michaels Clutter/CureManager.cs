using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureManager : MonoBehaviour
{
    public int cureAmount = 0;
    public int allCures;

    private void Start()
    {
        cureAmount = 0;
    }

    public void IncreaseCureCount()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.ItemPickup, this.transform.position);
        cureAmount++; 
        Debug.Log(cureAmount);
    }
}
