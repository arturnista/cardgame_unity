using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCardArea : ScriptableObject
{
    
    public abstract List<Vector3> GetAreaOfEffect(Vector3 castPosition, int value);

}
