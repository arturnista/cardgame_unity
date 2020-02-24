using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Enemies/New Attack action")]
public class EnemyAttackAction : BaseEnemyAction
{
    
    [Header("Attack")]
    [SerializeField] private int _damage = default;

    public override void ExecutePerTarget(GameObject target)
    {
        Entity entityTarget = target.GetComponent<Entity>();
        Debug.Log(target.name);

        IHealth targetHealth = entityTarget.Health;
        IModifiersHolder targetModifiers = entityTarget.ModifiersHolder;
        IModifiersHolder selfModifiers = _self.GetComponent<IModifiersHolder>();

        Damage damage = DamageCalculator.DealDamage(_damage, selfModifiers, targetHealth, targetModifiers);

        targetHealth.DealDamage(damage);
    }

    public override EnemyIntent GetIntent()
    {
        EnemyIntent intent = base.GetIntent();
        intent.Value = _damage;
        return intent;
    }

}
