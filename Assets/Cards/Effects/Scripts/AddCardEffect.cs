using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/Effects/Add card")]
public class AddCardEffect : BaseCardEffect
{

    public enum PositionToAdd
    {
        Draw,
        Discard,
        Hand
    }

    [SerializeField] private Card _cardToAdd = default;
    [SerializeField] private PositionToAdd _addTo = PositionToAdd.Draw;

    public override void OnTargetPlay(GameObject target, int[] values)
    {
        PlayerEntity playerEntity = GameObject.FindObjectOfType<PlayerEntity>();
        if (_addTo == PositionToAdd.Draw) playerEntity.PlayerDeck.AddCardToDraw(_cardToAdd);
        else if (_addTo == PositionToAdd.Discard) playerEntity.PlayerDeck.AddCardToDicard(_cardToAdd);
        else if (_addTo == PositionToAdd.Hand) playerEntity.PlayerDeck.AddCardToHand(_cardToAdd);
    }

}
