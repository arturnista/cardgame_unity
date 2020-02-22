using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Deal Damage")]
public class DealDamageEffect : BaseCardEffect
{

    public override void OnTargetPlay(int value, GameObject target)
    {
        IHealth targetHealth = target.GetComponent<IHealth>();
        IModifiersHolder targetModifiers = target.GetComponent<IModifiersHolder>();
        IModifiersHolder damagerModifiers = GameObject.FindObjectOfType<PlayerController>().GetComponent<IModifiersHolder>();

        Damage damage = DamageCalculator.DealDamage(value, damagerModifiers, targetHealth, targetModifiers);

        targetHealth.DealDamage(damage);
    }

}