using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/New RagnarokCard", fileName="RagnarokCard")]
public class RagnarokCard : BaseCard
{
    
    [Header("RagnarokCard")]
    [SerializeField] protected int m_Damage = default;
    public int Damage { get => m_Damage; protected set => m_Damage = value; }
    [SerializeField] protected string m_CardToAdd = default;
    public string CardToAdd { get => m_CardToAdd; protected set => m_CardToAdd = value; }

    public override void Initialize()
    {
        base.Initialize();
        
        m_OnPlayEffects = new List<BaseCardEffect>();
        m_OnPlayEffects.Add(new DealDamageEffect(m_CastLayer, m_Damage));
        m_OnPlayEffects.Add(new AddCardEffect(m_CastLayer, m_CardToAdd, PlayerDeck.DeckPiles.Draw));
        
        m_OnPostPlayEffects.Clear();
        m_OnPostPlayEffects.Add(new ExaustCardEffect(m_CastLayer));
    }

}
