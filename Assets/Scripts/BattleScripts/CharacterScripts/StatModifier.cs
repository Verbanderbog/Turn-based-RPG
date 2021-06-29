using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModType
{
    BaseFlat = 100,
    PercentAdd = 200,
    PercentMult = 300,
    Flat = 400,
}
[CreateAssetMenu]
public class StatModifier : ScriptableObject
{
    public float Value;
    public StatModType ModType;
    public int Order;
    [SerializeField] private bool customOrder = false;
    public object Source;
    public StatType Stat;
    

    private void OnValidate()
    {
        if (!customOrder)
            Order = (int)ModType;
    }

    public StatModifier(float value, StatModType type, int order, object source, StatType stat) 
    {
        Value = value;
        ModType = type;
        Order = order;
        Source = source;
        Stat = stat;
    }

    public StatModifier(float value, StatModType type, int order, object source) : this(value, type, order, source, StatType.Default) { }
    public StatModifier(float value, StatModType type, object source, StatType stat) : this(value, type, (int)type, source, stat) { }
    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }

    // Requires Value, Type and Order. Sets Source to its default value: null
    public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

    // Requires Value, Type and Source. Sets Order to its default value: (int)Type
    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }

}
