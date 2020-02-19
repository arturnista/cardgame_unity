using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Modifiers/Vulnerable modifier")]
public class VulnerableModifier : BaseCardModifier
{

    [SerializeField] [Range(1f, 2f)] private float _reduceAmount;

    public override bool OnEndTurn(GameObject self)
    {
        m_Amount -= 1;
        return m_Amount <= 0;
    }

    public override int ModifyTakeDamage(int damage)
    {
        return Mathf.CeilToInt(damage * _reduceAmount);
    }

}
