using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCardEffect : ScriptableObject
{
    
    [SerializeField] [TextArea] protected string m_Description = default;

    [SerializeField] protected List<EntityType> m_CastLayer = default;
    public List<EntityType> CastLayer { get => m_CastLayer; protected set => m_CastLayer = value; }

    public virtual void OnPlay(List<Vector3> castPositions, int[] values)
    {

        GameController gameController = DI.Get<GameController>();

        foreach (var target in gameController.GetEntitiesAtPositions(castPositions))
        {
            EntityTypeHolder typeHolder = target.GetComponent<EntityTypeHolder>();
            if (typeHolder.IsType(m_CastLayer))
            {
                OnTargetPlay(target, values);
            }
        }

    }
    
    public abstract void OnTargetPlay(GameObject target, int[] values);

    public string GetDescription(int[] values)
    {
        return string.Format(m_Description, values.Select(x=>x.ToString()).ToArray());
    }

}