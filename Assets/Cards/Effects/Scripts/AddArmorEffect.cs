using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Add Armor")]
public class AddArmorEffect : BaseCardEffect
{

    public override void OnTargetPlay(GameObject target, int[] values)
    {
        target.GetComponent<Entity>().Health.Armor += values[0];
    }

}