using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    
    [SerializeField] private int _handCards = 5;
    [SerializeField] private CardDeck _deck = default;

    public enum DeckPiles
    {
        Hand,
        Discard,
        Draw,
        Exaust
    }

    private List<BaseCard> m_DrawPile;
    public List<BaseCard> DrawPile { get => m_DrawPile; }
    
    private List<BaseCard> m_Hand;
    public List<BaseCard> Hand { get => m_Hand; }
    
    private List<BaseCard> m_DiscardPile;
    public List<BaseCard> DiscardPile { get => m_DiscardPile; }
    
    private List<BaseCard> m_ExaustPile;
    public List<BaseCard> ExaustPile { get => m_ExaustPile; }

    private GameController _gameController;
    private PlayerEntity _playerEntity;

    void Awake()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
        _playerEntity = GetComponent<PlayerEntity>();

        m_DrawPile = new List<BaseCard>();
        m_Hand = new List<BaseCard>();
        m_ExaustPile = new List<BaseCard>();
        m_DiscardPile = new List<BaseCard>(_deck.Cards);    
    }

    public void StartGame()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowDrawPile();
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            ShowDiscardPile();
        } else if (Input.GetKeyDown(KeyCode.X))
        {
            ShowExaustPile();
        }

    }

    public void ShowDrawPile()
    {
        List<BaseCard> drawShuffled = new List<BaseCard>(m_DrawPile);
        drawShuffled.Shuffle();
        UICardList.Main.Show("Draw pile", drawShuffled);
    }

    public void ShowDiscardPile()
    {
        UICardList.Main.Show("Discard pile", m_DiscardPile);
    }

    public void ShowExaustPile()
    {
        UICardList.Main.Show("Exaust pile", m_ExaustPile);
    }

    public void PlayCard(int index, Vector3 point)
    {
        BaseCard card = m_Hand[index];
        if (card.ManaCost > _playerEntity.PlayerHealth.ManaAmount)
        {
            DebugText.ShowText("NOT ENOUGH MANA");
            return;
        }

        int distance = Mathf.RoundToInt(Vector3.Distance(point, transform.position));
        if (distance > card.CastRange)
        {
            DebugText.ShowText("MAX RANGE");
            return;
        }
        
        _playerEntity.PlayerHealth.ManaAmount -= card.ManaCost;

        card.Play(point, transform.position);
        card.PostPlay(_playerEntity);

        _gameController.UIUpdateCards();
    }

    public void MoveCard(BaseCard card, DeckPiles deckPile)
    {
        m_Hand.Remove(card);

        switch (deckPile)
        {
            case DeckPiles.Hand:
                m_Hand.Add(card);
                break;
            case DeckPiles.Discard:
                m_DiscardPile.Add(card);
                break;
            case DeckPiles.Draw:
                m_DrawPile.Add(card);
                break;
            case DeckPiles.Exaust:
                m_ExaustPile.Add(card);
                break;
        }
    }

    void ShuffleDiscardIntoDraw()
    {
        while (m_DiscardPile.Count > 0)
        {
            int rand = Random.Range(0, m_DiscardPile.Count);
            m_DrawPile.Add(m_DiscardPile[rand]);
            m_DiscardPile.RemoveAt(rand);
        }
    }

    public void StartTurn()
    {
        for (int i = 0; i < _handCards; i++)
        {
            if (m_DrawPile.Count == 0) ShuffleDiscardIntoDraw();
            m_Hand.Add(m_DrawPile[0]);
            m_DrawPile.RemoveAt(0);
        }
    }

    public void EndTurn()
    {
        for (int i = m_Hand.Count - 1; i >= 0 ; i--)
        {
            m_Hand[i].EndTurn(_playerEntity);
        }

        // while(m_Hand.Count > 0)
        // {
        //     m_DiscardPile.Add(m_Hand[0]);
        //     m_Hand.RemoveAt(0);
        // }
    }

    public void AddCardToHand(string cardId)
    {
        BaseCard card = DI.Get<CardDatabase>().GetNewCard(cardId);
        m_Hand.Add(card);
    }

    public void AddCardToDraw(string cardId)
    {
        BaseCard card = DI.Get<CardDatabase>().GetNewCard(cardId);
        m_DrawPile.Insert(Random.Range(0, m_DrawPile.Count), card);
    }

    public void AddCardToDiscard(string cardId)
    {
        BaseCard card = DI.Get<CardDatabase>().GetNewCard(cardId);
        m_DiscardPile.Add(card);
    }

}
