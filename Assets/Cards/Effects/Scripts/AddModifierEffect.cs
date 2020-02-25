using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddModifierEffect : BaseCardEffect
{

    private BaseModifier _modifier = default;
    private int _amount = default;

    public AddModifierEffect(List<EntityType> castLayer, BaseModifier modifier, int amount) : base(castLayer)
    {
        _modifier = modifier;
        _amount = amount;
    }

    public override void OnTargetPlay(GameObject target)
    {
        target.GetComponent<EntityModifiers>().AddModifier(_modifier, _amount);
    }

    public override string GetDescription()
    {
        return string.Format("Add {0} {1}", _amount, _modifier.Title);
    }

}