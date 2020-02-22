using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCardEffect : ScriptableObject
{
    
    [SerializeField] [TextArea] protected string m_Description = default;

    [SerializeField] protected List<EntityType> m_CastLayer = default;
    public List<EntityType> CastLayer { get => m_CastLayer; protected set => m_CastLayer = value; }

    public virtual void OnPlay(int value, List<Vector3> castPositions)
    {

        GameController gameController = DI.Get<GameController>();

        foreach (var target in gameController.GetEntitiesAtPositions(castPositions))
        {
            EntityTypeHolder typeHolder = target.GetComponent<EntityTypeHolder>();
            if (typeHolder.IsType(m_CastLayer))
            {
                OnTargetPlay(value, target);
            }
        }

    }
    
    public abstract void OnTargetPlay(int value, GameObject target);

    public string GetDescription(int value)
    {
        return string.Format(m_Description, value);
    }

}