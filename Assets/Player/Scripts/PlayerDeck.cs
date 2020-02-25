using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    
    [SerializeField] private CardDeck _deck = default;

    private List<BaseCard> m_Draw;
    public List<BaseCard> Draw { get => m_Draw; }
    
    private List<BaseCard> m_Hand;
    public List<BaseCard> Hand { get => m_Hand; }
    
    private List<BaseCard> m_Discard;
    public List<BaseCard> Discard { get => m_Discard; }
    
    private List<BaseCard> m_Exaust;
    public List<BaseCard> Exaust { get => m_Exaust; }

    private GameController _gameController;
    private PlayerHealth _playerHealth;

    void Awake()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
        _playerHealth = GetComponent<PlayerHealth>();

        m_Draw = new List<BaseCard>();
        m_Hand = new List<BaseCard>();
        m_Exaust = new List<BaseCard>();
        m_Discard = new List<BaseCard>(_deck.Cards);    
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
        }

    }

    public void ShowDrawPile()
    {
        List<BaseCard> drawShuffled = new List<BaseCard>(m_Draw);
        drawShuffled.Shuffle();
        UICardList.Main.Show("Draw pile", drawShuffled, KeyCode.A);
    }

    public void ShowDiscardPile()
    {
        UICardList.Main.Show("Discard pile", m_Discard, KeyCode.D);
    }

    public void PlayCard(int index, Vector3 point)
    {
        BaseCard card = m_Hand[index];
        if (card.ManaCost > _playerHealth.ManaAmount)
        {
            DebugText.ShowText("NOT ENOUGH MANA");
            return;
        }
        
        _playerHealth.ManaAmount -= card.ManaCost;

        card.Play(point, transform.position);

        m_Hand.RemoveAt(index);
        if (card.ExaustOnPlay)
        {
            m_Exaust.Add(card);
        }
        else
        {
            m_Discard.Add(card);
        }

        _gameController.DrawCards();
    }

    public void ShuffleHand()
    {
        while (m_Discard.Count > 0)
        {
            int rand = Random.Range(0, m_Discard.Count);
            m_Draw.Add(m_Discard[rand]);
            m_Discard.RemoveAt(rand);
        }
    }

    public void StartTurn()
    {
        for (int i = 0; i < 5; i++)
        {
            if (m_Draw.Count == 0) ShuffleHand();
            m_Hand.Add(m_Draw[0]);
            m_Draw.RemoveAt(0);
        }
    }

    public void EndTurn()
    {
        while(m_Hand.Count > 0)
        {
            m_Discard.Add(m_Hand[0]);
            m_Hand.RemoveAt(0);
        }
    }

    public void AddCardToHand(BaseCard card)
    {
        m_Hand.Add(card);
    }

    public void AddCardToDraw(BaseCard card)
    {
        m_Draw.Insert(Random.Range(0, m_Draw.Count), card);
    }

    public void AddCardToDicard(BaseCard card)
    {
        m_Discard.Add(card);
    }

}
