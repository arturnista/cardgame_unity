using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Enemies/New Armor action")]
public class EnemyArmorAction : BaseEnemyAction
{
    
    [Header("Armor")]
    [SerializeField] private int _armor = default;

    public override bool ExecutePerTarget(GameObject target)
    {
        IHealth targetHealth = target.GetComponent<IHealth>();
        targetHealth.Armor += _armor;
        return true;
    }

}
