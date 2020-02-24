using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Modifiers/Empower modifier")]
public class EmpowerModifier : BaseModifier
{

    public override bool OnEndTurn(GameObject self)
    {
        m_Amount -= 1;
        return m_Amount <= 0;
    }

    public override int ModifyDealDamage(int damage)
    {
        return Mathf.CeilToInt(damage + m_Amount);
    }

}