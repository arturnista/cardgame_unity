using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/New HeavySlashCard", fileName="HeavySlashCard")]
public class HeavySlashCard : BaseCard
{
    
    [Header("HeavySlashCard")]
    [SerializeField] protected int m_Damage = default;
    public int Damage { get => m_Damage; protected set => m_Damage = value; }
    [SerializeField] protected string m_CardIdToAdd = default;
    public string CardIdToAdd { get => m_CardIdToAdd; protected set => m_CardIdToAdd = value; }

    public override void Initialize()
    {
        base.Initialize();
        
        m_OnPlayEffects = new List<BaseCardEffect>();
        m_OnPlayEffects.Add(new DealDamageEffect(m_CastLayer, m_Damage));
        m_OnPlayEffects.Add(new AddCardEffect(m_CastLayer, m_CardIdToAdd, PlayerDeck.DeckPiles.Draw));
        
        m_OnPostPlayEffects.Clear();
        m_OnPostPlayEffects.Add(new ExaustCardEffect(m_CastLayer));
    }

}
