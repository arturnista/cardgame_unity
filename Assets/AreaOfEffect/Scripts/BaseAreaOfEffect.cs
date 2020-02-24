using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAreaOfEffect : ScriptableObject
{
    
    public abstract List<Vector3> GetAreaOfEffect(Vector3 castPosition, Vector3 casterPosition, int value);

}
