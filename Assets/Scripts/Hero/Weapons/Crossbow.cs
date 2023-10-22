using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    public override int DealDamage()
    {
        if (Random.Range(MinChance, MaxChance) < CritChance)
        {
            float value = Damage * CritDamage;
            return (int)value;
        }

        return Damage;
    }

    public override void Shoot(Transform shootPoint)
    {
        Instantiate(Bullet, shootPoint.position, Quaternion.identity);
    }
}
