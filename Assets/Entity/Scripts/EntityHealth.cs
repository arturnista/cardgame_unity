using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntityHealth : MonoBehaviour, IHealth
{
    
    public delegate void DeathHandler(GameObject enemy);
    public DeathHandler OnDeath;

    public delegate void DamageHandler(GameObject enemy);
    public DamageHandler OnDamage;

    [SerializeField] protected int m_MaxHealth;
    public int MaxHealth
    {
        get => m_MaxHealth;
        set => m_MaxHealth = value;
    }

    protected int m_Health;
    public int Health
    {
        get => m_Health;
        set => m_Health = value;
    }

    protected int m_Armor;
    public int Armor
    {
        get => m_Armor;
        set => m_Armor = value;
    }

    void Awake()
    {
        Health = m_MaxHealth;
    }
    
    public virtual void DealDamage(Damage damage)
    {        
        Health -= damage.Health;
        Armor -= damage.Armor;
        if (OnDamage != null)
        {
            OnDamage(gameObject);
        }

        if (Health <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        if (OnDeath != null)
        {
            OnDeath(gameObject);
        }
    }

}
