using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/New Slash", fileName="SlashCard")]
public class SlashCard : BaseCard
{
    
    [Header("Slash")]
    [SerializeField] protected int m_Damage = default;
    public int Damage { get => m_Damage; protected set => m_Damage = value; }

    public override void Initialize()
    {
        m_OnPlayEffects = new List<BaseCardEffect>();
        m_OnPlayEffects.Add(new DealDamageEffect(m_CastLayer, m_Damage));
    }

}
