using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAI : BaseEnemyAI
{
    
    [SerializeField] private float _distanceToRange = 2f;
    [SerializeField] private BaseEnemyAction _rangeAttack = default;
    [SerializeField] private BaseEnemyAction _meleeAttack = default;
    [SerializeField] private BaseEnemyAction _strongShieldAttack = default;
    [SerializeField] private BaseEnemyAction _weakShieldAttack = default;

    private bool _lastActionWasSuccessful;
    
    protected override BaseEnemyAction DefineNextAction()
    {
        if (m_LastAction?.ID == _meleeAttack.ID)
        {
            if (_lastActionWasSuccessful)
            {
                return _strongShieldAttack;
            }
            else
            {
                return _weakShieldAttack;
            }
        }
        else if (Vector3.Distance(_playerEntity.transform.position, transform.position) > _distanceToRange)
        {
            return _rangeAttack;
        }
        else
        {
            return _meleeAttack;
        }
    }

    public override bool Execute()
    {
        _lastActionWasSuccessful = base.Execute();
        return _lastActionWasSuccessful;
    }

}
