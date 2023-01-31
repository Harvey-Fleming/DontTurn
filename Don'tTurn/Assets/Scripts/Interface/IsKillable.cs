using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IsKillable
{
    void OnHit(int damageTaken);

    void OnDeath();
}
