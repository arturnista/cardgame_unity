using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/New BattleHungerCard", fileName="BattleHungerCard")]
public class BattleHungerCard : BaseCard
{
    
    [Header("Battle Hunger")]
    [SerializeField] protected int m_SelfDamage = default;
    [SerializeField] protected int m_ManaGain = default;
    public int Damage { get => m_SelfDamage; protected set => m_SelfDamage = value; }
    public int ManaGain { get => m_ManaGain; protected set => m_ManaGain = value; }

    public override void Initialize()
    {
        base.Initialize();
        
        m_OnPlayEffects = new List<BaseCardEffect>();
        m_OnPlayEffects.Add(new SelfDamageEffect(m_CastLayer, m_SelfDamage));
        m_OnPlayEffects.Add(new AddManaEffect(m_CastLayer, m_ManaGain));
    }

}
