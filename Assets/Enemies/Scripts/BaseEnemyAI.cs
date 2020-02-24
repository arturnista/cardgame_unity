using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseEnemyAI : MonoBehaviour
{

    protected BaseEnemyAction m_NextAction;
    public BaseEnemyAction NextAction { get => m_NextAction; }

    protected Entity _entity;
    protected EnemyHealth _enemyHealth;

    protected PlayerEntity _playerEntity;
    protected EnemiesController _enemiesController;

    protected virtual void Awake()
    {
        _playerEntity = GameObject.FindObjectOfType<PlayerEntity>();
        _enemiesController = GameObject.FindObjectOfType<EnemiesController>();

        _entity = GetComponent<Entity>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    public void PrepareNextAction()
    {
        if (_enemyHealth.IsDead) return;
        
        BaseEnemyAction nextAction = DefineNextAction();
        m_NextAction = Instantiate(nextAction);
        m_NextAction.Prepare(_playerEntity, _enemiesController, gameObject);
    }

    protected abstract BaseEnemyAction DefineNextAction();

    public void Execute()
    {
        if (_enemyHealth.IsDead) return;
        
        m_NextAction.Execute();
        m_NextAction = null;
    }

}
