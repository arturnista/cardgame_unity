using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : BaseCardEffect
{

    private int _damage;

    public DealDamageEffect(List<EntityType> castLayer, int damage) : base(castLayer)
    {
        _damage = damage;
    }

    public override void OnTargetPlay(GameObject target)
    {
        IHealth targetHealth = target.GetComponent<IHealth>();
        IModifiersHolder targetModifiers = target.GetComponent<IModifiersHolder>();
        IModifiersHolder damagerModifiers = GameObject.FindObjectOfType<PlayerEntity>().ModifiersHolder;

        Damage damage = DamageCalculator.DealDamage(_damage, damagerModifiers, targetHealth, targetModifiers);

        targetHealth.DealDamage(damage);
    }

    public override string GetDescription()
    {
        return string.Format("Deal {0} damage", _damage);
    }

}