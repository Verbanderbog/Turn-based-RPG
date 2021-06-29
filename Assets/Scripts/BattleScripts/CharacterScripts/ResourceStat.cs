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
    public new void AddModifier(StatModifier mod)
    {
        base.AddModifier(mod);
    }
    public new void RemoveModifier(StatModifier mod)
    {
        base.RemoveModifier(mod);
    }
}
