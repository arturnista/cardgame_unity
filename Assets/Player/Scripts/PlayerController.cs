using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHealth
{
    
    [SerializeField] private CardDeck _deck = default;
    [SerializeField] private int m_MaxHealth;
    public int MaxHealth { get => m_MaxHealth; protected set => m_MaxHealth = value; }
    
    private int m_Health;
    public int Health { get => m_Health; protected set => m_Health = value; }
    
    private int m_Armor;
    public int Armor { get => m_Armor; set => m_Armor = value; }

    private List<Card> m_Draw;
    public List<Card> Draw { get => m_Draw; }
    
    private List<Card> m_Hand;
    public List<Card> Hand { get => m_Hand; }
    
    private List<Card> m_Discard;
    public List<Card> Discard { get => m_Discard; }

    private int m_ManaAmount;
    public int ManaAmount { get => m_ManaAmount; }
    
    private GameController _gameController;
    private EnemiesController _enemiesController;

    void Awake()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();
        _enemiesController = GameObject.FindObjectOfType<EnemiesController>();

        m_Health = m_MaxHealth;

        m_Draw = new List<Card>();
        m_Hand = new List<Card>();
        m_Discard = new List<Card>(_deck.Cards);      
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
        List<Card> drawShuffled = new List<Card>(m_Draw);
        drawShuffled.Shuffle();
        UICardList.Main.Show("Draw pile", drawShuffled, KeyCode.A);
    }

    public void ShowDiscardPile()
    {
        UICardList.Main.Show("Discard pile", m_Discard, KeyCode.D);
    }

    public void PlayCard(Card card, Vector3 point)
    {
        if (card.ManaCost > m_ManaAmount)
        {
            DebugText.ShowText("NOT ENOUGH MANA");
            return;
        }
        
        m_ManaAmount -= card.ManaCost;

        card.Play(point);

        m_Hand.Remove(card);
        m_Discard.Add(card);

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
        m_ManaAmount = 3;
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

    public virtual void DealDamage(Damage damage)
    {        
        Health -= damage.Health;
        Armor -= damage.Armor;
    }

}
