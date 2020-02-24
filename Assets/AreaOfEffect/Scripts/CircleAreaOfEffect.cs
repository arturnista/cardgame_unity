using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Area of effect/Circle", fileName="CircleAreaOfEffect")]
public class CircleAreaOfEffect : BaseAreaOfEffect
{

    [SerializeField] private bool _castSelf = false;

    public override List<Vector3> GetAreaOfEffect(Vector3 castPosition, Vector3 casterPosition, int value)
    {
        Vector3 referencePosition = _castSelf ? casterPosition : castPosition;
        float powValue = Mathf.Pow(value, 2);

        List<Vector3> result = new List<Vector3>();
        for (int x = -value; x <= value; x++)
        {
            for (int y = -value; y <= value; y++)
            {
                Vector3 tilePosition = new Vector3(x, y);
                if (tilePosition.sqrMagnitude <= powValue)
                {
                    result.Add(referencePosition + tilePosition);
                }
            }
        }
        return result;
    }

}
