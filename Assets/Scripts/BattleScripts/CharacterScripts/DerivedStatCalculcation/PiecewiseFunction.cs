using System;
using System.Collections;
using System.Collections.Generic;


public class PiecewiseFunction
{
    private readonly Dictionary<float[], Function> functions = new Dictionary<float[], Function>();

    public PiecewiseFunction(params Coordinate[] coordinates)
    {
        for (int i = 0; i < coordinates.Length - 1; i++)
        {
            functions.Add(new float[]{coordinates[i].x,coordinates[i+1].x}, new Function(coordinates[i], coordinates[i + 1]));
        }
        functions.Add(new float[] { coordinates[coordinates.Length - 1].x, Int16.MaxValue }, new Function(coordinates[coordinates.Length - 2], coordinates[coordinates.Length - 1]));
    }
    public float solve(int x)
    {
        var keys = functions.Keys;

        foreach (float[] i in keys)
        {

            if (x>= i[0] && x<i[1])
            {
                return functions[i].solve(x);
            }
        }
        return 0;
    }
}
