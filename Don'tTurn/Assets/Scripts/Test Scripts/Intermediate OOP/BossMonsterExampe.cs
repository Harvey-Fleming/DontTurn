using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterExampe : MonoBehaviour
{
    BossMonster boss = null;
    BossMonster sameBoss = null;

    // Start is called before the first frame update
    void Start()
    {
        boss = new BossMonster("Freddy", 100);
        sameBoss = boss;
        boss = null;
    }

    // Update is called once per frame
    void OnDisable()
    {
        sameBoss = null;
    }
}
