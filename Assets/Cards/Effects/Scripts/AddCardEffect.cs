using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardEffect : BaseCardEffect
{

    public enum PileToAdd
    {
        Draw,
        Discard,
        Hand
    }

    private BaseCard _cardToAdd = default;
    private PileToAdd _addTo = PileToAdd.Draw;

    public AddCardEffect(List<EntityType> castLayer, BaseCard card, PileToAdd pile) : base(castLayer)
    {
        _cardToAdd = card;
        _addTo = pile;
    }

    public override void OnPlay(BaseCard card, List<Vector3> castPositions)
    {
        PlayerEntity playerEntity = GameObject.FindObjectOfType<PlayerEntity>();
        if (_addTo == PileToAdd.Draw) playerEntity.PlayerDeck.AddCardToDraw(_cardToAdd);
        else if (_addTo == PileToAdd.Discard) playerEntity.PlayerDeck.AddCardToDicard(_cardToAdd);
        else if (_addTo == PileToAdd.Hand) playerEntity.PlayerDeck.AddCardToHand(_cardToAdd);
    }

    public override string GetDescription()
    {
        string addToString = "";
        switch (_addTo)
        {
            case PileToAdd.Discard:
                addToString = "Discard pile";
                break;
            case PileToAdd.Draw:
                addToString = "Draw pile";
                break;
            case PileToAdd.Hand:
                addToString = "Hand";
                break;
        }
        return string.Format("Add a {0} to your {1}", _cardToAdd.Title, addToString);
    }

}
