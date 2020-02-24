using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyAction : ScriptableObject
{

    [SerializeField] protected BaseAreaOfEffect m_AreaOfEffect;
    public BaseAreaOfEffect AreaOfEffect { get => m_AreaOfEffect; }
    
    [SerializeField] protected int m_AreaOfEffectValue;
    public int AreaOfEffectValue { get => m_AreaOfEffectValue; }

    [SerializeField] protected List<EntityType> m_CastLayer = default;
    public List<EntityType> CastLayer { get => m_CastLayer; protected set => m_CastLayer = value; }

    [Header("Intent")]
    [SerializeField] protected Sprite m_IntentIcon;
    public Sprite IntentIcon { get => m_IntentIcon; }

    [SerializeField] protected Color m_IntentIconColor;
    public Color IntentIconColor { get => m_IntentIconColor; }
    
    protected PlayerEntity _playerEntity;
    protected EnemiesController _enemiesController;
    protected GameObject _self;

    protected List<Vector3> _castPositions;

    public virtual void Prepare(PlayerEntity playerEntity, EnemiesController enemiesController, GameObject self)
    {
        _playerEntity = playerEntity;
        _enemiesController = enemiesController;
        _self = self;

        _castPositions = GetAreaOfEffect(_playerEntity, _self);
    }

    public virtual void Execute()
    {
        GameController gameController = DI.Get<GameController>();
        foreach (var target in gameController.GetEntitiesAtPositions(_castPositions))
        {
            EntityTypeHolder typeHolder = target.GetComponent<EntityTypeHolder>();
            if (typeHolder.IsType(m_CastLayer))
            {
                ExecutePerTarget(target);
            }
        }

    }

    public abstract void ExecutePerTarget(GameObject entity);

    public virtual EnemyIntent GetIntent()
    {
        return new EnemyIntent(m_IntentIcon, m_IntentIconColor, GetAreaOfEffect(_playerEntity, _self));
    }

    protected virtual List<Vector3> GetAreaOfEffect(PlayerEntity playerEntity, GameObject self)
    {
        return m_AreaOfEffect.GetAreaOfEffect(playerEntity.transform.position, self.transform.position, m_AreaOfEffectValue);
    }

}
