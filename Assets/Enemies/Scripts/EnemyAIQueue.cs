using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyAIQueue : BaseEnemyAI
{

    [SerializeField] private List<BaseEnemyAction> _actions = default;
    private Queue<BaseEnemyAction> _actionQueue;

    protected override void Awake()
    {
        base.Awake();
        _actionQueue = new Queue<BaseEnemyAction>();

        List<BaseEnemyAction> _enemiesAux = new List<BaseEnemyAction>(_actions);
        while (_enemiesAux.Count > 0)
        {            
            _actionQueue.Enqueue(_enemiesAux[0]);
            _enemiesAux.RemoveAt(0);
        }
    }

    protected override BaseEnemyAction DefineNextAction()
    {
        BaseEnemyAction nextAction = _actionQueue.Dequeue();
        _actionQueue.Enqueue(nextAction);
        return nextAction;
    }

}
