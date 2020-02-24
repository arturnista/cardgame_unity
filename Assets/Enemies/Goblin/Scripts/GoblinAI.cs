using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAI : BaseEnemyAI
{
    
    [SerializeField] private BaseEnemyAction _rangeAttack = default;
    [SerializeField] private BaseEnemyAction _meleeAttack = default;
    [SerializeField] private BaseEnemyAction _strongShieldAttack = default;
    [SerializeField] private BaseEnemyAction _weakShieldAttack = default;
    
    protected override BaseEnemyAction DefineNextAction()
    {
        if (Vector3.Distance(_playerEntity.transform.position, transform.position) > 2f)
        {
            return _rangeAttack;
        }
        else
        {
            return _meleeAttack;
        }
    }

}
