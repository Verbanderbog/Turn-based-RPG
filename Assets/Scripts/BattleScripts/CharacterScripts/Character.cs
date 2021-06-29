
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu]
public class Character : ScriptableObject
{
    //Scriptable input
    public string Name = "";
    public short Level = 1;
    public int HealthMax = 1;
    public int ActionPointsMax =1;
    public short Strength; //determines defense, crit damage and melee damage
    public short Speed; //determines turn order, crit chance and dodge chance
    public short Aim; //determines hit chance, crit chance and ranged damage
    public short Logic; //determines ability damage, crit damage, a little melee damage, and a little ranged damage

    public List<Move> DefaultMoveList;

    public HashSet<Move> MoveSet;

    public byte team = 1;

    //Exposed Stats
    public ResourceStat _health;
    public ResourceStat _actionPoints;
    public ExposedStat _strength; //determines defense, crit damage and melee damage
    public ExposedStat _speed; //determines turn order, crit chance and dodge chance
    public ExposedStat _aim; //determines hit chance, crit chance and ranged damage
    public ExposedStat _logic; //determines ability damage, crit damage, a little melee damage, and a little ranged damage
    //Derived Stats
    public DerivedStat _defense;
    public DerivedStat _dodge;
    public DerivedStat _critDamage;
    public DerivedStat _critChance;
    public DerivedStat _hitChance;
    public DerivedStat _melee;
    public DerivedStat _ranged;
    public DerivedStat _ability;

    


    private void OnValidate()
    {
        _health = new ResourceStat(HealthMax, StatType.Health);
        _actionPoints = new ResourceStat(ActionPointsMax, StatType.ActionPoints);
        _strength = new ExposedStat(Strength, StatType.Strength, _defense, _critDamage, _melee);
        _speed = new ExposedStat(Speed, StatType.Speed, _critChance, _dodge);
        _aim = new ExposedStat(Aim, StatType.Aim, _hitChance, _critChance, _ranged);
        _logic = new ExposedStat(Logic, StatType.Logic, _ability, _critDamage, _melee, _ranged);

        _defense = new DerivedStat(StatType.Defense, 0.9f, 0f, _strength);
        _dodge = new DerivedStat(StatType.Dodge, 5f, -5f, _speed);
        _critDamage = new DerivedStat(StatType.CritDamage, 4f, 1f, _strength, _logic);
        _critChance = new DerivedStat(StatType.CritChance, 4f, 0f, _speed, _aim);
        _hitChance = new DerivedStat(StatType.HitChance, 4f, 0f, _aim);
        _melee = new DerivedStat(StatType.Melee, 1000f, 1f, _strength, _logic);
        _ranged = new DerivedStat(StatType.Ranged, 1000f, 1f, _aim, _logic);
        _ability = new DerivedStat(StatType.Ability, 1000f, 1f, _logic);

        MoveSet = new HashSet<Move>();
        MoveSet.UnionWith(DefaultMoveList);
        DefaultMoveList = MoveSet.ToList();

        Debug.Log(Name + ", " + Level + "/n" + _health.ToString()+"/n"+_actionPoints.ToString()+"/n"+_strength.ToString()+"/n"+_speed.ToString()+"/n"+_aim.ToString()+"/n"+_logic.ToString() + "/n" + _defense.ToString() + "/n" + _dodge.ToString() + "/n" + _critDamage.ToString() + "/n" + _critChance.ToString() + "/n" + _hitChance.ToString() + "/n" + _melee.ToString() + "/n" + _ranged.ToString() + "/n" + _ability.ToString());
        
    }
    public void addStatModifiers(params StatModifier[] statModifiers)
    {

        foreach (StatModifier i in statModifiers)
        {
            switch (i.Stat)
            {
                case StatType.Health:
                    _health.AddModifier(i);
                    break;
                case StatType.ActionPoints:
                    _actionPoints.AddModifier(i);
                    break;
                case StatType.Strength:
                    _strength.AddModifier(i);
                    break;
                case StatType.Speed:
                    _speed.AddModifier(i);
                    break;
                case StatType.Aim:
                    _aim.AddModifier(i);
                    break;
                case StatType.Logic:
                    _logic.AddModifier(i);
                    break;
                case StatType.Defense:
                    _defense.AddModifier(i);
                    break;
                case StatType.Dodge:
                    _dodge.AddModifier(i);
                    break;
                case StatType.CritDamage:
                    _critDamage.AddModifier(i);
                    break;
                case StatType.CritChance:
                    _critChance.AddModifier(i);
                    break;
                case StatType.HitChance:
                    _hitChance.AddModifier(i);
                    break;
                case StatType.Melee:
                    _melee.AddModifier(i);
                    break;
                case StatType.Ranged:
                    _ranged.AddModifier(i);
                    break;
                case StatType.Ability:
                    _ability.AddModifier(i);
                    break;
                default:
                    break;
            }
        }
    }
    public void removeStatModifiers(params StatModifier[] statModifiers)
    {

        foreach (StatModifier i in statModifiers)
        {
            switch (i.Stat)
            {
                case StatType.Health:
                    _health.RemoveModifier(i);
                    break;
                case StatType.ActionPoints:
                     _actionPoints.RemoveModifier(i);
                    break;
                case StatType.Strength:
                     _strength.RemoveModifier(i);
                    break;
                case StatType.Speed:
                     _speed.RemoveModifier(i);
                    break;
                case StatType.Aim:
                     _aim.RemoveModifier(i);
                    break;
                case StatType.Logic:
                     _logic.RemoveModifier(i);
                    break;
                case StatType.Defense:
                     _defense.RemoveModifier(i);
                    break;
                case StatType.Dodge:
                     _dodge.RemoveModifier(i);
                    break;
                case StatType.CritDamage:
                     _critDamage.RemoveModifier(i);
                    break;
                case StatType.CritChance:
                     _critChance.RemoveModifier(i);
                    break;
                case StatType.HitChance:
                     _hitChance.RemoveModifier(i);
                    break;
                case StatType.Melee:
                     _melee.RemoveModifier(i);
                    break;
                case StatType.Ranged:
                     _ranged.RemoveModifier(i);
                    break;
                case StatType.Ability:
                     _ability.RemoveModifier(i);
                    break;
                default:
                    break;
            }
        }
    }
    
    public void removeStatModifiersBySource(object source)
    {
        _health.RemoveAllModifiersFromSource(source);
        _actionPoints.RemoveAllModifiersFromSource(source);
        _strength.RemoveAllModifiersFromSource(source);
        _speed.RemoveAllModifiersFromSource(source);
        _aim.RemoveAllModifiersFromSource(source);
        _logic.RemoveAllModifiersFromSource(source);
        _defense.RemoveAllModifiersFromSource(source);
        _dodge.RemoveAllModifiersFromSource(source);
        _critDamage.RemoveAllModifiersFromSource(source);
        _critChance.RemoveAllModifiersFromSource(source);
        _hitChance.RemoveAllModifiersFromSource(source);
        _melee.RemoveAllModifiersFromSource(source);
        _ranged.RemoveAllModifiersFromSource(source);
        _ability.RemoveAllModifiersFromSource(source);

    }


    public void takeDamage(OutgoingDamage damage)
    {
        float damageDelta = 0f;
        float hit = Random.Range(0f,1f);
        float levelDifferenceModifier = (Level - damage.Level) <= 5 ? 0 : Mathf.Pow(Level - damage.Level - 5,2)*0.0125f;
        float effectiveHitChance = damage.HitChance - levelDifferenceModifier - (0.5f * _dodge.Value);
        int critHitStage = 0;
        int hitStage = 0;
        if (hit < effectiveHitChance) //Does the attack hit the target?
        {
            hitStage = 2;
            float crit = Random.Range(0f, 1f);
            critHitStage = (int) Mathf.Floor(damage.CritChance);
            if (crit < (damage.CritChance-critHitStage))//Does the attack Crit and at what level?
                critHitStage++;
            float critHitStageMultiplier = Mathf.Pow(critHitStage, 3f / 5f); //Turns the crit damage stage into an appropriate multiplier
            damageDelta += damage.Value * damage.CritDamage * critHitStageMultiplier;

            //apply defense
            damageDelta -= (damage.Value + damageDelta) * _defense.Value;

            float graze = Random.Range(0f, 1f);
            float effectiveGrazeChance = (damage.HitChance > 1) ? _dodge.Value - ((damage.HitChance - 1) * 0.75f) : _dodge.Value;
            if (graze < effectiveGrazeChance) //Does the attack graze the target?
            {
                hitStage = 1;
                damageDelta -= (damage.Value+damageDelta) * 0.25f; //Reduce the damage by 25%
                if ((damageDelta + damage.Value) > _health.CurrentAmount && _health.CurrentAmount > Mathf.Ceil(_health.Value * 0.01f)) // grazes cannot kill unless the target is at less than 1% max health
                    damageDelta = _health.CurrentAmount - 1;
            }
            else if (effectiveHitChance > 1) //can the attack Direct Hit. Must be over 100% effective hit chance
            {
                //Does the attack direct hit and at what level? Only hits over 100% effective hit chance can direct hit.
                float direct = Random.Range(0f, 1f);
                int directHitStage = (int) Mathf.Floor(effectiveHitChance); //Stage is one higher than it should be b/c first 100% should not count as a stage
                if (!(direct < (effectiveHitChance - directHitStage))) //If the random chance to direct hit does not proc, lower the stage by one level.
                    directHitStage--;
                damageDelta += Mathf.Ceil(directHitStage * 0.03f * _health.CurrentAmount); // Direct hit does 3% of the target's current HP per level of direct hit.
                hitStage += directHitStage;
            }
            _health.reduceResource((int)Mathf.Round(damage.Value + damageDelta));
        }
        
        //TODO: call method for UI damage popup. Pass hitStage, critHitStage, and final damage
    }

}
