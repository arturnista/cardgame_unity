using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/New Card Deck")]
public class CardDeck : ScriptableObject
{
    
    [SerializeField] private List<Card> _cards = default;
    public List<Card> Cards { get => _cards; }

}
