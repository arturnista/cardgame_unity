using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCardEffect
{
    
    protected string m_Description = default;

    protected List<EntityType> m_CastLayer = default;
    public List<EntityType> CastLayer { get => m_CastLayer; protected set => m_CastLayer = value; }

    protected BaseCardEffect(List<EntityType> castLayer)
    {
        m_CastLayer = castLayer;
    }

    public virtual void OnPlay(BaseCard card, List<Vector3> castPositions)
    {

        GameController gameController = DI.Get<GameController>();

        foreach (var target in gameController.GetEntitiesAtPositions(castPositions))
        {
            EntityTypeHolder typeHolder = target.GetComponent<EntityTypeHolder>();
            if (typeHolder.IsType(m_CastLayer))
            {
                OnTargetPlay(target);
            }
        }

    }
    
    public virtual void OnTargetPlay(GameObject target)
    {

    }

    public virtual void Execute(BaseCard card, PlayerEntity playerEntity)
    {
        
    }

    public abstract string GetDescription();

}