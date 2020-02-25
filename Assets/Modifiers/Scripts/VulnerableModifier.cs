using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Modifiers/Vulnerable modifier")]
public class VulnerableModifier : BaseModifier
{

    [SerializeField] [Range(1f, 2f)] private float _reduceAmount = default;

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
