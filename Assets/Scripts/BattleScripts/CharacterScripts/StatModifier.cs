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
    public bool customOrder = false;
    public int Order;
    public object Source;
    public StatType Stat;
    

    private void OnValidate()
    {
        if (!customOrder)
            Order = (int)ModType;
    }


}
