﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/New HeavySlashCard", fileName="HeavySlashCard")]
public class HeavySlashCard : BaseCard
{
    
    [Header("HeavySlashCard")]
    [SerializeField] protected int m_Damage = default;
    public int Damage { get => m_Damage; protected set => m_Damage = value; }
    [SerializeField] protected BaseCard m_CardToAdd = default;
    public BaseCard CardToAdd { get => m_CardToAdd; protected set => m_CardToAdd = value; }

    void OnEnable()
    {
        m_OnPlayEffects = new List<BaseCardEffect>();
        m_OnPlayEffects.Add(new DealDamageEffect(m_CastLayer, m_Damage));
        m_OnPlayEffects.Add(new AddCardEffect(m_CastLayer, m_CardToAdd, AddCardEffect.PileToAdd.Draw));
    }

}