using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase
{
    
    private Dictionary<string, BaseCard> _cards;

    public CardDatabase()
    {
        _cards = new Dictionary<string, BaseCard>();
    }

    public void AddCard(BaseCard card)
    {
        _cards.Add(card.ID, card);
    }

    public BaseCard GetCardTemplate(string id)
    {
        if (!_cards.ContainsKey(id)) return null;
        return _cards[id];
    }

    public BaseCard GetNewCard(string id)
    {
        if (!_cards.ContainsKey(id)) return null;
        BaseCard card = ScriptableObject.Instantiate(_cards[id]);
        card.Initialize();
        return card;
    }

}
