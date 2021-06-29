using System.Collections;
using UnityEngine;



public class OutgoingDamage 
{
    public int Value;
    public int CritDamage;
    public float HitChance;
    public float CritChance;
    public int Level;

    public OutgoingDamage(int damage, int critDamage, float hitChance, float critChance, int level)
    {
        float damageRandomness = Random.Range(damage * -0.03f, damage * 0.03f);
        damageRandomness = damageRandomness > 0 ? Mathf.Ceil(damageRandomness) : Mathf.Floor(damageRandomness);
        Value = damage + (int) damageRandomness;
        CritDamage = critDamage > 1 ? critDamage : 1;
        HitChance = hitChance > 0 ? hitChance : 0;
        CritChance = critChance > 0 ? critChance : 0;
        Level = level > 1 ? level : 1;
    }
}