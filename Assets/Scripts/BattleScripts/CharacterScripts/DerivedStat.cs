using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerivedStat : CharacterStat
{
    public new float BaseValue { get {
            if (baseDirty)
            {
                baseDirty = false;
                base.BaseValue = 0;
                var keys = ParentStatFunctions.Keys;
                foreach(ExposedStat parent in keys)
                {
                    base.BaseValue += ParentStatFunctions[parent].solve((int) parent.BaseValue);
                }
            }
            return base.BaseValue; 
        } set { base.BaseValue = value; } }

    private readonly Dictionary<ExposedStat, PiecewiseFunction> ParentStatFunctions = new Dictionary<ExposedStat, PiecewiseFunction>();


    private bool baseDirty = true;
    public DerivedStat(StatType name, float max, float min, params ExposedStat[] parentStats) : base(0, name, max, min)
    {
        
        switch (name)
        {
            case StatType.Defense:
                for (int i=0;i<parentStats.Length;i++)
                {
                    Debug.Log(name.ToString());
                    if (parentStats[i].Name == StatType.Strength)
                    {
                        ParentStatFunctions.Add(parentStats[i],new PiecewiseFunction(new Coordinate(0, 0), new Coordinate(10,0.2f), new Coordinate(30,0.40f), new Coordinate(70,0.55f), new Coordinate(100,0.675f)));
                    } else
                    {
                        throw new Exception();
                    }
                }
                break;
            case StatType.Dodge:
                for (int i = 0; i < parentStats.Length; i++)
                {
                    Debug.Log(name.ToString());
                    if (parentStats[i].Name == StatType.Speed)
                    {
                        ParentStatFunctions.Add(parentStats[i], new PiecewiseFunction(new Coordinate(0, 0), new Coordinate(10, 0.25f), new Coordinate(30, 0.55f), new Coordinate(60, 0.70f), new Coordinate(100, 0.85f)));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                break;
            case StatType.CritDamage:
                for (int i = 0; i < parentStats.Length; i++)
                {
                    Debug.Log(name.ToString());
                    if (parentStats[i].Name == StatType.Strength)
                    {
                        ParentStatFunctions.Add(parentStats[i], new PiecewiseFunction(new Coordinate(0, 0.1f), new Coordinate(100, 0.6f)));
                    }
                    else if (parentStats[i].Name == StatType.Logic)
                    {
                        ParentStatFunctions.Add(parentStats[i], new PiecewiseFunction(new Coordinate(0, 0.1f), new Coordinate(100, 0.45f)));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                ParentStatFunctions.Add(new ExposedStat(0,StatType.Default), new PiecewiseFunction(new Coordinate(0, 1.0f), new Coordinate(100, 1.0f)));
                break;
            case StatType.CritChance:
                for (int i = 0; i < parentStats.Length; i++)
                {
                    Debug.Log(name.ToString());
                    if (parentStats[i].Name == StatType.Aim )
                    {
                        ParentStatFunctions.Add(parentStats[i], new PiecewiseFunction(new Coordinate(0, 0), new Coordinate(100,0.90f)));
                    }
                    else if (parentStats[i].Name == StatType.Speed)
                    {
                        ParentStatFunctions.Add(parentStats[i], new PiecewiseFunction(new Coordinate(0, 0), new Coordinate(100, 0.90f)));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                break;
            case StatType.HitChance:
                for (int i = 0; i < parentStats.Length; i++)
                {
                    Debug.Log(name.ToString());
                    if (parentStats[i].Name == StatType.Aim)
                    {
                        ParentStatFunctions.Add(parentStats[i], new PiecewiseFunction(new Coordinate(0, 0.9f), new Coordinate(100,2f)));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                break;
            case StatType.Melee:
                for (int i = 0; i < parentStats.Length; i++)
                {
                    Debug.Log(name.ToString());
                    if (parentStats[i].Name == StatType.Strength)
                    {
                        ParentStatFunctions.Add(parentStats[i], new PiecewiseFunction(new Coordinate(0, 1), new Coordinate(100, 251)));
                    }
                    else if (parentStats[i].Name == StatType.Logic)
                    {
                        ParentStatFunctions.Add(parentStats[i], new PiecewiseFunction(new Coordinate(0, 0), new Coordinate(100, 150)));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                break;
            case StatType.Ranged:
                for (int i = 0; i < parentStats.Length; i++)
                {
                    Debug.Log(name.ToString());
                    if (parentStats[i].Name == StatType.Aim)
                    {
                        ParentStatFunctions.Add(parentStats[i], new PiecewiseFunction(new Coordinate(0, 1), new Coordinate(100, 251)));
                    }
                    else if (parentStats[i].Name == StatType.Logic)
                    {
                        ParentStatFunctions.Add(parentStats[i], new PiecewiseFunction(new Coordinate(0, 0), new Coordinate(100, 150)));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                break;
            case StatType.Ability:
                for (int i = 0; i < parentStats.Length; i++)
                {
                    Debug.Log(name.ToString());
                    if ( parentStats[i].Name == StatType.Logic)
                    {
                        ParentStatFunctions.Add(parentStats[i], new PiecewiseFunction(new Coordinate(0, 1), new Coordinate(100, 401)));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                break;
            default:
                break;
        }
        var keys = ParentStatFunctions.Keys;
        foreach (ExposedStat parent in keys)
        {
            base.BaseValue += ParentStatFunctions[parent].solve((int)parent.BaseValue);
        }
    }

    public void dirtyBase()
    {
        baseDirty = true;
    }
}
