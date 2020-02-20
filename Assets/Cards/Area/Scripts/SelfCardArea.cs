using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Area/Self")]
public class SelfCardArea : BaseCardArea
{

    public override List<Vector3> GetAreaOfEffect(Vector3 castPosition, Vector3 casterPosition, int value)
    {
        return new List<Vector3>() { casterPosition };
    }

}
