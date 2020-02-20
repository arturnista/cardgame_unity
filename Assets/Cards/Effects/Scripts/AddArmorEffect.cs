using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Add Armor")]
public class AddArmorEffect : BaseCardEffect
{

    [SerializeField] private int _armor = 5;

    public override void OnTargetPlay(GameObject target)
    {
        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        playerController.Armor += _armor;
    }

}