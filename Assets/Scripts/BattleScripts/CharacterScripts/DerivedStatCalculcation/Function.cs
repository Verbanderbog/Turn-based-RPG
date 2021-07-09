using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function
{
    private float m;
    private float b;

    public Function(int m, int b)
    {
        this.m = m;
        this.b = b;
    }

    public Function(Coordinate one, Coordinate two)
    {
        m = Mathf.Round(((two.y - one.y) / (float) (two.x - one.x))*100000)/100000;
        b = Mathf.Round((one.y - (m * one.x)) * 100000)/ 100000;
        //Debug.Log("y = " + m + "x + " + b);
    }

    public float solve(int x)
    {
        return (m*x+b);
    }
}
