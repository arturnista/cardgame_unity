using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddManaEffect : BaseCardEffect
{

    private int _amount;

    public AddManaEffect(List<EntityType> castLayer, int amount) : base(castLayer)
    {
        _amount = amount;
    }

    public override void OnPlay(BaseCard card, List<Vector3> castPositions)
    {
        PlayerHealth playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        playerHealth.ManaAmount += _amount;
    }

    public override string GetDescription()
    {
        return string.Format("Add {0} energy", _amount);
    }

}