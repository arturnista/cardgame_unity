using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Deal Damage To All")]
public class DealDamageAllEnemiesEffect : BaseCardEffect
{

    [SerializeField] private int _damage = 5;

    public override void OnPlay(GameObject target)
    {
        EnemiesController enemiesController = GameObject.FindObjectOfType<EnemiesController>();
        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        for (int i = enemiesController.Enemies.Count - 1; i >= 0 ; i--)
        {
            GameObject enemy = enemiesController.Enemies[i];
            
            IHealth targetHealth = enemy.GetComponent<IHealth>();
            IModifiersHolder targetModifiers = enemy.GetComponent<IModifiersHolder>();
            IModifiersHolder damagerModifiers = playerController.GetComponent<IModifiersHolder>();

            Damage damage = DamageCalculator.DealDamage(_damage, damagerModifiers, targetHealth, targetModifiers);
            targetHealth.DealDamage(damage);
        }
    }

}