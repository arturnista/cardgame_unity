using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCardEffect : BaseCardEffect
{

    public DiscardCardEffect(List<EntityType> castLayer) : base(castLayer)
    {
    }

    public override void OnPlay(BaseCard card, List<Vector3> castPositions)
    {
    }

    public override void Execute(BaseCard card, PlayerEntity playerEntity)
    {
        playerEntity.PlayerDeck.MoveCard(card, PlayerDeck.DeckPiles.Discard);
    }

    public override string GetDescription()
    {
        return string.Format("Exaust");
    }

}
