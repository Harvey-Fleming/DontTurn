using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosses : MonoBehaviour
{
    void OnDisable()
    {
        IceBoss IceBoss = new IceBoss();
        FireBoss FireBoss = new FireBoss();

        IceBoss.Shoot();
        FireBoss.Shoot();

    }

}
