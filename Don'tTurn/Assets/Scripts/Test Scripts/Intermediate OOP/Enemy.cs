using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    protected int hitPoints;
    public enum DamageType { Water, Fire};

    public virtual void TakeDamage(int amount, DamageType damagetype)
    {
        hitPoints -= amount;
    }

    public string GetHitPoints()
    {
        return "" + hitPoints;
    }
}

class WaterEnemy : Enemy
{
    public override void TakeDamage(int amount, DamageType damagetype)
    {
        if (damagetype == DamageType.Fire)
        {
            base.TakeDamage(amount * 2, damagetype);
        }
        else
        {
            base.TakeDamage(amount / 2, damagetype);
        }
    }

    class FireEnemy : Enemy
    {
        public override void TakeDamage(int amount, DamageType damagetype)
        {
            if (damagetype == DamageType.Water)
            {
                base.TakeDamage(amount * 10, damagetype);
            }
            else
            {
                base.TakeDamage(amount / 0, damagetype);
            }
        }
    }
}
