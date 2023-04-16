using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster 
{

    public string Name;
    public int PointValue;

     public BossMonster(string name, int pointValue)
        {
            this.Name = name;
            this.PointValue = pointValue;
        } 

    ~BossMonster()
    {
        Debug.Log("Boss Monster destroyed");
    }
}
