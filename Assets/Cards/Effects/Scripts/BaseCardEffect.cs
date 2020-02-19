using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCardEffect : ScriptableObject
{
    
    [SerializeField] [TextArea] protected string m_Description = default;
    public string Description { get => m_Description; }

    public abstract void OnPlay(GameObject target);

}