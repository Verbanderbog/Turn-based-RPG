using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DamageType
{
    None,
    SingleTarget,
    SingleTargetOverTime,
    Area,
    AreaOverTime,
}
public enum EffectPlacement
{
    CenteredOnTargets,
    CenteredOnFirstTarget,
    CenteredOnCharacter,
}
[CreateAssetMenu]
public class Move : ScriptableObject
{
    public string Name;
    public string Description;
    public int ActionCost;
    [Serializable]
    public class MoveVisualEffect
    {
        public EffectPlacement placement;
        public VisualEffectBehavior visualEffect;
    }

    public MoveVisualEffect[] visualEffects;

}
