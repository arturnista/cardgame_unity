using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDamageEffect : BaseCardEffect
{
    private int _selfdamage;

    public SelfDamageEffect(List<EntityType> castLayer, int selfdamage) : base(castLayer)
    {
        _selfdamage = selfdamage;
    }

    public override void OnPlay(BaseCard card, List<Vector3> castPositions)
    {
        PlayerHealth playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        Damage damage = new Damage(_selfdamage, 0);
        playerHealth.DealDamage(damage);
    }

    public override string GetDescription()
    {
        return string.Format("Take {0} damage", _selfdamage);
    }
}
