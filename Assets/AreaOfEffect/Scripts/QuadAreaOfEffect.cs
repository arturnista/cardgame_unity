using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Area of effect/Quad", fileName="QuadAreaOfEffect")]
public class QuadAreaOfEffect : BaseAreaOfEffect
{

    [SerializeField] private bool _castSelf = false;

    public override List<Vector3> GetAreaOfEffect(Vector3 castPosition, Vector3 casterPosition, int value)
    {        
        Vector3 referencePosition = _castSelf ? casterPosition : castPosition;

        List<Vector3> result = new List<Vector3>();
        for (int x = -value; x <= value; x++)
        {
            for (int y = -value; y <= value; y++)
            {
                result.Add(referencePosition + new Vector3(x, y));
            }
        }
        return result;
    }

}
