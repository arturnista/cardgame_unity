using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Area/All Enemies")]
public class AllEnemiesCardArea : BaseCardArea
{

    public override List<Vector3> GetAreaOfEffect(Vector3 castPosition, Vector3 casterPosition, int value)
    {
        Vector3 bounds = DI.Get<MapController>().WorldBounds();
        Vector2Int halfBounds = new Vector2Int(
            Mathf.RoundToInt((bounds.x - 1) / 2f),
            Mathf.RoundToInt((bounds.y - 1) / 2f)
        );

        List<Vector3> result = new List<Vector3>();
        for (int x = -halfBounds.x; x <= halfBounds.x; x++)
        {
            for (int y = -halfBounds.y; y <= halfBounds.y; y++)
            {
                result.Add(Vector3.one * 0.5f + new Vector3(x, y));
            }
        }
        return result;
    }

}
