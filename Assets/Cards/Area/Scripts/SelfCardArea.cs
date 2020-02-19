using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Area/Self")]
public class SelfCardArea : BaseCardArea
{

    public override List<Vector3> GetAreaOfEffect(Vector3 castPosition, int value)
    {
        return new List<Vector3>() { castPosition };
    }

}
