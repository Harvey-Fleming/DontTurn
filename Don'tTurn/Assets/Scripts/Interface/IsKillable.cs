using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IsKillable
{
    void OnHit(float damageTaken, GameObject attacker);

    void OnDeath();
}
