using System;
using System.Collections.Generic;
using UnityEngine;


public enum StatType
{
    Default,
    Health,
    ActionPoints,
    Strength,
    Speed,
    Aim,
    Logic,
    Defense,
    Dodge,
    CritDamage,
    CritChance,
    HitChance,
    Melee,
    Ranged,
    Ability,
}
 public class CharacterStat
{
    private float _baseValue;
    public float BaseValue { get { return _baseValue; } set {  _baseValue = value; } }
    public float Value {
        get
        {
            if (modifierDirty)
            {
                _value = CalculateFinalValue();
                modifierDirty = false;
            }
            return _value;
        }
    }

    public StatType Name;

    private bool modifierDirty = true;
    private float _value;

    private readonly List<StatModifier> statModifiers;

    private readonly float max;
    private readonly float min;

    public CharacterStat(float baseValue, StatType name, float max, float min)
    {
        _baseValue = baseValue;
        Name = name;
        statModifiers = new List<StatModifier>();
        this.max = max;
        this.min = min;
    }

    

    public void AddModifier(StatModifier mod)
    {
        modifierDirty = true;
        mod.Stat = Name;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    // Add this method to the CharacterStat class
    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0; // if (a.Order == b.Order)
    }

    public void RemoveModifier(StatModifier mod)
    {
        modifierDirty = true;
        statModifiers.Remove(mod);
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                modifierDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float deltaValue = 0;
        float sumPercentAdd = 0; // This will hold the sum of our "PercentAdd" modifiers

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.ModType == StatModType.BaseFlat)
            {
                finalValue += mod.Value;
            }
            else if (mod.ModType == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier
            {
                sumPercentAdd += mod.Value; // Start adding together all modifiers of this type

                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].ModType != StatModType.PercentAdd)
                {
                    deltaValue += finalValue * sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            }
            else if (mod.ModType == StatModType.PercentMult) // Percent renamed to PercentMult
            {
                finalValue *= 1 + mod.Value;
            }
            else if (mod.ModType == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
        }
        finalValue += deltaValue;
        if (finalValue > max)
            return max;
        else if (finalValue < min)
            return min;
        return (float)Math.Round(finalValue, 4);
    }

    public override string ToString()
    {
        return Name.ToString() + ": " + Value;
    }
}
