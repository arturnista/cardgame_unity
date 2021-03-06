using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Modifiers/Poison modifier")]
public class PoisonModifier : BaseModifier
{

    public override bool OnStartTurn(GameObject self)
    {
        Damage damage = new Damage(m_Amount, 0);
        self.GetComponent<IHealth>().DealDamage(damage);
        m_Amount -= 1;
        DebugText.ShowText("POISON DEALT " + (m_Amount + 1) + " DAMAGE | " + (m_Amount <= 0 ? "finished" : "going") );
        return m_Amount <= 0;
    }

}
