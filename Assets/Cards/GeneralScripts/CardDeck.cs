using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/New Card Deck")]
public class CardDeck : ScriptableObject
{
    
    [SerializeField] private List<BaseCard> _cards = default;
    public List<BaseCard> Cards { get => _cards; }

}
