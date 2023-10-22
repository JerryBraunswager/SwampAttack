using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Boomerang : Weapon
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
