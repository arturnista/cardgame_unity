using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    [SerializeField] private GameObject _cardTemplate = default;
    [SerializeField] private GameObject _cardParent;

    private PlayerController _playerController;
    private EnemiesController _enemiesController;

    private Card m_SelectedCard;
    public Card SelectedCard { get => m_SelectedCard; }

    void Awake()
    {
        DI.Set<GameController>(this);
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _enemiesController = GameObject.FindObjectOfType<EnemiesController>();
    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        _playerController.StartGame();
        _enemiesController.StartGame();
        EndEnemiesTurn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndPlayerTurn();
        }
    }

    void StartPlayerTurn()
    {
        _playerController.StartTurn();
        DrawCards();
    }

    public void EndPlayerTurn()
    {
        _playerController.EndTurn();
        DrawCards();

        StartEnemiesTurn();
    }

    void StartEnemiesTurn()
    {
        _enemiesController.StartTurn();
    }

    public void EndEnemiesTurn()
    {
        _enemiesController.EndTurn();
        
        StartPlayerTurn();
    }

    public void SelectCard(Card card)
    {
        m_SelectedCard = card;
    }

    public void SelectPoint(Vector3 point)
    {
        if (m_SelectedCard != null)
        {
            _playerController.PlayCard(m_SelectedCard, point);
        }
    }

    public void DrawCards()
    {
        foreach (Transform child in _cardParent.transform)
        {
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < _playerController.Hand.Count; i++)
        {
            UICardView ui = Instantiate(_cardTemplate,  _cardParent.transform).GetComponent<UICardView>();
            ui.Construct(_playerController.Hand[i], i, this);
        }
    }

}
