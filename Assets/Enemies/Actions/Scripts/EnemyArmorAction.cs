using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Enemies/New Armor action")]
public class EnemyArmorAction : BaseEnemyAction
{
    
    [Header("Armor")]
    [SerializeField] private int _armor = default;

    public override void Execute(PlayerController playerController, EnemiesController enemiesController, GameObject self)
    {
        IHealth targetHealth = self.GetComponent<IHealth>();
        targetHealth.Armor += _armor;
    }

}
