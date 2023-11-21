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

    public override void Shoot(Transform shootPoint, Menu menu)
    {
        var bullet = Instantiate(Bullet, shootPoint.position, Quaternion.identity);
        bullet.Init(menu);
    }
}
