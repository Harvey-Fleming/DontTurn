using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss 
{
    protected string attack;

    public void Shoot()
    {
        Debug.Log(attack);
    }

}

public class IceBoss : Boss
{
    public IceBoss()
    {
        attack = "Ice Attack";
    }
}

public class FireBoss : Boss
{
    public FireBoss()
    {
        attack = "Fire Attack";
    }
}