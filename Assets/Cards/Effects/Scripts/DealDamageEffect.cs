using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Deal Damage")]
public class DealDamageEffect : BaseCardEffect
{

    [SerializeField] private int _damage = 5;

    public override void OnPlay(GameObject target)
    {
        IHealth targetHealth = target.GetComponent<IHealth>();
        IModifiersHolder targetModifiers = target.GetComponent<IModifiersHolder>();
        IModifiersHolder damagerModifiers = GameObject.FindObjectOfType<PlayerController>().GetComponent<IModifiersHolder>();

        Damage damage = DamageCalculator.DealDamage(_damage, damagerModifiers, targetHealth, targetModifiers);

        targetHealth.DealDamage(damage);
    }

}