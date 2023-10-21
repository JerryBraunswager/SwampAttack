using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    private int minChance = 0;
    private int maxChance = 101;

    public override int DealDamage()
    {
        if (Random.Range(minChance, maxChance) < _attributes[2].Value)
        {
            float value = _attributes[1].Value * _attributes[2].Value;
            return (int)value;
        }

        return (int)_attributes[1].Value;
    }

    public override void Shoot(Transform shootPoint)
    {
        Animator = FindObjectOfType<Player>().GetComponent<Animator>();
        Animator.Play("Bow");
        Instantiate(Bullet, shootPoint.position, Quaternion.identity);
    }
}
