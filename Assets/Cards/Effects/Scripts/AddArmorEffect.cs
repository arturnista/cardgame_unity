using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Add Armor")]
public class AddArmorEffect : BaseCardEffect
{

    public override void OnTargetPlay(int value, GameObject target)
    {
        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        playerController.Armor += value;
    }

}