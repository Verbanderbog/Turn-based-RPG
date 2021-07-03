using System.Collections;
using System.Collections.Generic;


public class ResourceStat : CharacterStat
{
    private float _currentAmount;
    public int CurrentAmount { get { return (int)_currentAmount; } }

    public ResourceStat(float baseValue, StatType name) : base(baseValue, name, 1, float.MaxValue)
    {
        _currentAmount = Value;
    }
    public void restore()
    {
        _currentAmount = Value;
    }
    public int reduceResource(int reduction)
    {
        return reduceResource(reduction, 0);
    }

    public int reduceResource(int reduction,int min)
    {
        _currentAmount -= reduction;
        if (_currentAmount < min)
            _currentAmount = min;
        return CurrentAmount;
    }
    public new void AddModifier(params StatModifier[] mods)
    {
        //TODO: apply modifiers to CurrentAmount
        base.AddModifier(mods);
    }
    public new void RemoveModifier(StatModifier mod)
    {
        //TODO: apply removal of modifier to CurrentAmount
        base.RemoveModifier(mod);
    }
    public new bool RemoveAllModifiersFromSource(object source)
    {
        //TODO: apply removal of modifiers to CurrentAmount
        return base.RemoveAllModifiersFromSource(source);
    }
}
