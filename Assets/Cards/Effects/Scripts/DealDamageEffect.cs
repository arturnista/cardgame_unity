using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Deal Damage")]
public class DealDamageEffect : BaseCardEffect
{

    public override void OnTargetPlay(GameObject target, int[] values)
    {
        IHealth targetHealth = target.GetComponent<IHealth>();
        IModifiersHolder targetModifiers = target.GetComponent<IModifiersHolder>();
        IModifiersHolder damagerModifiers = GameObject.FindObjectOfType<PlayerEntity>().ModifiersHolder;

        Damage damage = DamageCalculator.DealDamage(values[0], damagerModifiers, targetHealth, targetModifiers);

        targetHealth.DealDamage(damage);
    }

}