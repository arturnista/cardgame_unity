using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyAction : ScriptableObject
{
    [Header("Intent")]

    [SerializeField] protected Sprite m_IntentIcon;
    public Sprite IntentIcon { get => m_IntentIcon; }

    [SerializeField] protected Color m_IntentIconColor;
    public Color IntentIconColor { get => m_IntentIconColor; }

    public abstract void Execute(PlayerController playerController, EnemiesController enemiesController, GameObject self);

    public virtual EnemyIntent GetIntent()
    {
        return new EnemyIntent(m_IntentIcon, m_IntentIconColor);
    }

}
