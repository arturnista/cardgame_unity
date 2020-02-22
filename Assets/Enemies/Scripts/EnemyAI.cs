using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private List<BaseEnemyAction> _actions = default;
    private Queue<BaseEnemyAction> _actionQueue;

    private EnemyHealth _enemyHealth;
    
    public BaseEnemyAction NextAction { get => _actionQueue.Peek(); }

    void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _actionQueue = new Queue<BaseEnemyAction>();

        List<BaseEnemyAction> _enemiesAux = new List<BaseEnemyAction>(_actions);
        while (_enemiesAux.Count > 0)
        {            
            _actionQueue.Enqueue(_enemiesAux[0]);
            _enemiesAux.RemoveAt(0);
        }
    }

    public void Execute(PlayerEntity playerEntity, EnemiesController enemiesController, GameObject self)
    {
        if (_enemyHealth.IsDead) return;
        
        BaseEnemyAction action = _actionQueue.Dequeue();
        action.Execute(playerEntity, enemiesController, self);
        _actionQueue.Enqueue(action);
    }

}
