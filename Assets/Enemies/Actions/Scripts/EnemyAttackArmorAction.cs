using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Enemies/New Attack Armor action")]
public class EnemyAttackArmorAction : BaseEnemyAction
{
    
    [Header("Attack Armor")]
    [SerializeField] private int _damage = default;
    [SerializeField] private int _armor = default;

    public override bool Execute()
    {
        IHealth selfHealth = _self.GetComponent<IHealth>();
        selfHealth.Armor += _armor;

        return base.Execute();
    }

    public override bool ExecutePerTarget(GameObject target)
    {
        Entity entityTarget = target.GetComponent<Entity>();

        IHealth targetHealth = entityTarget.Health;
        IModifiersHolder targetModifiers = entityTarget.ModifiersHolder;
        IModifiersHolder selfModifiers = _self.GetComponent<IModifiersHolder>();

        Damage damage = DamageCalculator.DealDamage(_damage, selfModifiers, targetHealth, targetModifiers);
        targetHealth.DealDamage(damage);

        return true;
    }

    public override EnemyIntent GetIntent()
    {
        EnemyIntent intent = base.GetIntent();
        intent.Value = _damage;
        return intent;
    }

}
