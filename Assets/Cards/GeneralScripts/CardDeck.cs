using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/New Card Deck")]
public class CardDeck : ScriptableObject
{
    
    [SerializeField] private List<string> _cardsIds = default;
    public List<BaseCard> Cards { get; protected set; }

    public void Prepare()
    {
        CardDatabase database = DI.Get<CardDatabase>();
        Cards = new List<BaseCard>();
        foreach (var id in _cardsIds)
        {
            Cards.Add(database.GetNewCard(id));
        }
    }

}
