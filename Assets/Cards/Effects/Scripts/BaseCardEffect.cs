using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCardEffect : ScriptableObject
{
    
    [SerializeField] [TextArea] protected string m_Description = default;
    public string Description { get => m_Description; }

    public virtual void OnPlay(List<Vector3> castPositions)
    {

        GameController gameController = DI.Get<GameController>();

        foreach (var target in gameController.GetEntitiesAtPositions(castPositions))
        {
            OnTargetPlay(target);
        }

    }
    
    public abstract void OnTargetPlay(GameObject target);

}