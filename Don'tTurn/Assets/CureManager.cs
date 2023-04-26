using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureManager : MonoBehaviour
{
    public int cureAmount = 0;
    public int allCures;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cureAmount == allCures) //&& TALK TO SCIENTIST OR SOMETHING IDK
        {
            Debug.Log("all cures acquired");
        }
    }
}
