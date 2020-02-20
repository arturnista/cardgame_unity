using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCardEffect : ScriptableObject
{
    
    [SerializeField] [TextArea] protected string m_Description = default;
    public string Description { get => m_Description; }

    public virtual void OnPlay(List<Vector3> castPositions)
    {

        EnemiesController enemiesController = DI.Get<EnemiesController>();

        foreach (var target in enemiesController.GetEnemiesAtPositions(castPositions))
        {
            OnTargetPlay(target);
        }

    }
    
    public abstract void OnTargetPlay(GameObject target);

}