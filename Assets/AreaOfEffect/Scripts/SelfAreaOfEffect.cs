﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Area of effect/Self", fileName="SelfAreaOfEffect")]
public class SelfAreaOfEffect : BaseAreaOfEffect
{

    public override List<Vector3> GetAreaOfEffect(Vector3 castPosition, Vector3 casterPosition, int value)
    {
        List<Vector3> result = new List<Vector3>();
        for (int x = -value; x <= value; x++)
        {
            for (int y = -value; y <= value; y++)
            {
                result.Add(casterPosition + new Vector3(x, y));
            }
        }
        return result;
    }

}