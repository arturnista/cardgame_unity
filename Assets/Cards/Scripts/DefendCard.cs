using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/New DefendCard", fileName="DefendCard")]
public class DefendCard : BaseCard
{
    
    [Header("DefendCard")]
    [SerializeField] protected int m_Armor = default;
    public int Armor { get => m_Armor; protected set => m_Armor = value; }

    void OnEnable()
    {
        m_OnPlayEffects = new List<BaseCardEffect>();
        m_OnPlayEffects.Add(new AddArmorEffect(m_CastLayer, m_Armor));
    }

}
