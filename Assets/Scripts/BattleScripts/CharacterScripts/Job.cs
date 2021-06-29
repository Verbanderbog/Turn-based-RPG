using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Job : ScriptableObject
{
    public List<StatModifier> StatModifiers;
    public List<Move> JobMoveList;

    private void OnValidate()
    {
        foreach (StatModifier i in StatModifiers)
        {
            i.Source = this;
        }
    }
}
