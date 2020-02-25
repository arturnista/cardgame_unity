using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddArmorEffect : BaseCardEffect
{

    private int _amount;

    public AddArmorEffect(List<EntityType> castLayer, int amount) : base(castLayer)
    {
        _amount = amount;
    }

    public override void OnTargetPlay(GameObject target)
    {
        target.GetComponent<Entity>().Health.Armor += _amount;
    }

    public override string GetDescription()
    {
        return string.Format("Add {0} armor", _amount);
    }

}