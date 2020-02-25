using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardEffect : BaseCardEffect
{

    private string _cardToAdd = default;
    private PlayerDeck.DeckPiles _addTo = PlayerDeck.DeckPiles.Draw;

    public AddCardEffect(List<EntityType> castLayer, string card, PlayerDeck.DeckPiles pile) : base(castLayer)
    {
        _cardToAdd = card;
        _addTo = pile;
    }

    public override void OnPlay(BaseCard card, List<Vector3> castPositions)
    {
        PlayerEntity playerEntity = GameObject.FindObjectOfType<PlayerEntity>();
        if (_addTo == PlayerDeck.DeckPiles.Draw) playerEntity.PlayerDeck.AddCardToDraw(_cardToAdd);
        else if (_addTo == PlayerDeck.DeckPiles.Discard) playerEntity.PlayerDeck.AddCardToDiscard(_cardToAdd);
        else if (_addTo == PlayerDeck.DeckPiles.Hand) playerEntity.PlayerDeck.AddCardToHand(_cardToAdd);
    }

    public override string GetDescription()
    {
        string addToString = "";
        switch (_addTo)
        {
            case PlayerDeck.DeckPiles.Discard:
                addToString = "Discard pile";
                break;
            case PlayerDeck.DeckPiles.Draw:
                addToString = "Draw pile";
                break;
            case PlayerDeck.DeckPiles.Hand:
                addToString = "Hand";
                break;
        }
        return string.Format("Add a {0} to your {1}", DI.Get<CardDatabase>().GetCardTemplate(_cardToAdd).Title, addToString);
    }

}
