using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Add modifier")]
public class AddModifierEffect : BaseCardEffect
{

    [SerializeField] private BaseCardModifier _modifier = default;
    [SerializeField] private int _amount = 1;
    [SerializeField] private bool _castOnPlayer = default;

    public override void OnTargetPlay(GameObject target)
    {
        if (_castOnPlayer)
        {
            IModifiersHolder holder = GameObject.FindObjectOfType<PlayerController>().GetComponent<IModifiersHolder>();
            holder.AddModifier(_modifier, _amount);
        }
        else
        {
            target.GetComponent<EntityModifiers>().AddModifier(_modifier, _amount);
        }
    }

}