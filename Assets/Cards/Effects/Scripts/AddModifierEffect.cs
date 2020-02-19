using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Add modifier")]
public class AddModifierEffect : BaseCardEffect
{

    [SerializeField] private BaseCardModifier _modifier = default;
    [SerializeField] private int _amount = 1;
    [SerializeField] private bool _castOnPlayer;

    public override void OnPlay(List<Vector3> castPositions)
    {
        if (_castOnPlayer)
        {
            IModifiersHolder holder = GameObject.FindObjectOfType<PlayerController>().GetComponent<IModifiersHolder>();
            holder.AddModifier(_modifier, _amount);
        }
        else
        {
            EnemiesController enemiesController = DI.Get<EnemiesController>();

            foreach (var target in enemiesController.GetEnemiesAtPositions(castPositions))
            {
                target.GetComponent<EntityModifiers>().AddModifier(_modifier, _amount);
            }
            
        }
    }

}