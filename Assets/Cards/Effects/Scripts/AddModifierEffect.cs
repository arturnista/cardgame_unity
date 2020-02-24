using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Add modifier")]
public class AddModifierEffect : BaseCardEffect
{

    [SerializeField] private BaseModifier _modifier = default;

    public override void OnTargetPlay(GameObject target, int[] values)
    {
        target.GetComponent<EntityModifiers>().AddModifier(_modifier, values[0]);
    }

}