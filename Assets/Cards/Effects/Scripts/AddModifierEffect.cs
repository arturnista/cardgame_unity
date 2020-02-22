using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Add modifier")]
public class AddModifierEffect : BaseCardEffect
{

    [SerializeField] private BaseCardModifier _modifier = default;

    public override void OnTargetPlay(int value, GameObject target)
    {
        target.GetComponent<EntityModifiers>().AddModifier(_modifier, value);
    }

}