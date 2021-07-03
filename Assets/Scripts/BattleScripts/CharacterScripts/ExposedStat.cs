using System;
using System.Collections;
using System.Collections.Generic;

public class ExposedStat : CharacterStat
{
    public new float BaseValue { get { return base.BaseValue; } set { base.BaseValue = value; dirtyDerived(); } }
    private readonly List<DerivedStat> DerivedStats;

    public ExposedStat(float baseValue, StatType name, float max, float min, params DerivedStat[] derivedStats) : base(baseValue, name, max, min)
    {
        DerivedStats = new List<DerivedStat>();
        DerivedStats.AddRange(derivedStats);
    }
    public ExposedStat(float baseValue, StatType name, params DerivedStat[] derivedStats) : base(baseValue, name, float.MaxValue, 0)
    {
        DerivedStats = new List<DerivedStat>();
        DerivedStats.AddRange(derivedStats);
    }

    private void dirtyDerived()
    {
        foreach(DerivedStat i in DerivedStats)
        {
            i.dirtyBase();
        }
    }

    public new void AddModifier(params StatModifier[] mods) 
    {
        dirtyDerived();
        base.AddModifier(mods);
    }
    public new void RemoveModifier(StatModifier mod)
    {
        dirtyDerived();
        base.RemoveModifier(mod);
        
    }
    public new bool RemoveAllModifiersFromSource(object source)
    {
        dirtyDerived();
        return base.RemoveAllModifiersFromSource(source);
    }

    
}