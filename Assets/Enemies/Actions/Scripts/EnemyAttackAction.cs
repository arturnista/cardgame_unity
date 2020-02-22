using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Enemies/New Attack action")]
public class EnemyAttackAction : BaseEnemyAction
{
    
    [Header("Attack")]
    [SerializeField] private int _damage = default;

    public override void Execute(PlayerEntity playerEntity, EnemiesController enemiesController, GameObject self)
    {
        IHealth targetHealth = playerEntity.Health;
        IModifiersHolder targetModifiers = playerEntity.ModifiersHolder;
        IModifiersHolder selfModifiers = self.GetComponent<IModifiersHolder>();

        Damage damage = DamageCalculator.DealDamage(_damage, selfModifiers, targetHealth, targetModifiers);

        targetHealth.DealDamage(damage);
    }

    public override EnemyIntent GetIntent()
    {
        return new EnemyIntent(m_IntentIcon, m_IntentIconColor, _damage);
    }

}
