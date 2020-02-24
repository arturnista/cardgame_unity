﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Area/All Enemies")]
public class AllEnemiesCardArea : BaseCardArea
{

    public override List<Vector3> GetAreaOfEffect(Vector3 castPosition, Vector3 casterPosition, int value)
    {
        List<Vector3Int> allTiles = DI.Get<MapController>().GetAllGroundTiles();
        List<Vector3> result = new List<Vector3>();
        foreach (Vector3Int pos in allTiles)
        {
            result.Add(Vector3.one * 0.5f + pos);
        }
        return result;
    }

}
