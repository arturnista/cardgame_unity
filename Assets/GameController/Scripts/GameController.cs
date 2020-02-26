using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


class CardSelection
{
    
    public CardSelection(int index, BaseCard card, UICardView uiCard)
    {
        UICard = uiCard;
        Card = card;
        Index = index;

        UICard.Select();
    }

    public UICardView UICard;
    public BaseCard Card;
    public int Index;

    public void Deselect()
    {
        UICard.Deselect();
    }

}

public class GameController : MonoBehaviour
{
    
    [SerializeField] private GameObject _playerEntityPrefab = default;
    [SerializeField] private GameObject _damageMarkerPrefab = default;
    [SerializeField] private GameObject _damageMarkerParent = default;
    [SerializeField] private LayerMask _entitiesLayerMask = default;
    [Header("UI")]
    [SerializeField] private GameObject _cardTemplate = default;
    [SerializeField] private GameObject _cardParent = default;
    [SerializeField] private GameObject _winCanvas = default;
    [SerializeField] private GameObject _defeatCanvas = default;

    private PlayerEntity _playerEntity;
    private EnemiesController _enemiesController;
    private MapController _mapController;

    private Camera _camera;

    private CardSelection m_SelectedCard;
    public BaseCard SelectedCard { get => m_SelectedCard?.Card; }

    private bool _isMoving = false;
    private bool _selectedCardThisFrame = false;
    private Vector3 _lastMousePosition;

    void Awake()
    {
        DI.Set<GameController>(this);
        
        _playerEntity = Instantiate(_playerEntityPrefab, new Vector3(.5f, -.5f), Quaternion.identity).GetComponent<PlayerEntity>();
        _camera = Camera.main;
        
        _winCanvas.SetActive(false);
        _defeatCanvas.SetActive(false);
    }

    void Start()
    {
        _enemiesController = DI.Get<EnemiesController>();
        _mapController = DI.Get<MapController>();

        _playerEntity.PlayerHealth.OnDeath += HandlePlayerDeath;
        _enemiesController.OnDefeatAllEnemies += HandleDefeatAllEnemies;
        
        StartGame();
    }

    void OnDisable()
    {
        _playerEntity.PlayerHealth.OnDeath -= HandlePlayerDeath;
        _enemiesController.OnDefeatAllEnemies -= HandleDefeatAllEnemies;
    }

    void StartGame()
    {
        _enemiesController.CreateEnemies();

        _playerEntity.StartGame();
        _enemiesController.StartGame();
        EndEnemiesTurn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndPlayerTurn();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeselectCard();
        }

        if (!_selectedCardThisFrame && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            SelectPoint(_mapController.WorldToCell(mousePosition));
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            CancelAction();
        }
    }

    void LateUpdate()
    {
        _selectedCardThisFrame = false;

        if (SelectedCard != null)
        {
            UIUpdateDamageArea();
        }
    }

    void HandleDefeatAllEnemies()
    {
        WinGame();
    }

    void HandlePlayerDeath(GameObject enemy)
    {
        DefeatGame();
    }

    void StartPlayerTurn()
    {
        _playerEntity.StartTurn();
        UIUpdateCards();
    }

    public void EndPlayerTurn()
    {
        _playerEntity.EndTurn();
        UIUpdateCards();

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

    public void SelectMoving()
    {
        _isMoving = true;
    }

    public void SelectCard(int index, BaseCard card, UICardView cardView)
    {
        if (card.ManaCost > _playerEntity.PlayerHealth.ManaAmount)
        {
            DebugText.ShowText("NOT ENOUGH MANA");
            return;
        }

        if (m_SelectedCard != null)
        {
            if (m_SelectedCard.Index == index)
            {
                DeselectCard();
                return;
            }
            else
            {
                m_SelectedCard.Deselect();
            }
        }

        m_SelectedCard = new CardSelection(index, card, cardView);

        _selectedCardThisFrame = true;
        UIUpdateDamageArea(true);
    }

    public void SelectPoint(Vector3 point)
    {
        if (m_SelectedCard != null)
        {
            _playerEntity.PlayerDeck.PlayCard(m_SelectedCard.Index, point);
            DeselectCard();
        }
        else if (_isMoving)
        {
            float distance = Vector3.Distance(_playerEntity.transform.position, point);
            int manaCost = Mathf.RoundToInt(distance / 3);
            if (manaCost > _playerEntity.PlayerHealth.ManaAmount)
            {
                DebugText.ShowText("NOT ENOUGH MANA");
                return;
            }
            _playerEntity.PlayerHealth.ManaAmount -=  manaCost;
            _playerEntity.Movement.MoveTo(point);
            _isMoving = false;
        }
    }

    public void DeselectCard()
    {
        if (m_SelectedCard != null)
        {
            m_SelectedCard.Deselect();
            m_SelectedCard = null;
        }

        HideDamageArea();
    }

    public void CancelAction()
    {
        DeselectCard();
        _isMoving = false;
    }

    public void UIUpdateCards()
    {
        foreach (Transform child in _cardParent.transform)
        {
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < _playerEntity.PlayerDeck.Hand.Count; i++)
        {
            UICardView ui = Instantiate(_cardTemplate,  _cardParent.transform).GetComponent<UICardView>();
            ui.Construct(_playerEntity.PlayerDeck.Hand[i], i, this);
        }
    }

    void UIUpdateDamageArea(bool force = false)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = _mapController.WorldToCell(mousePosition);
        if (!force && _lastMousePosition == mousePosition)
        {
            return;
        }
        _lastMousePosition = mousePosition;

        HideDamageArea();
        foreach (var pos in SelectedCard.GetAreaOfEffect(mousePosition, _playerEntity.transform.position))
        {
            Instantiate(_damageMarkerPrefab, pos, Quaternion.identity, _damageMarkerParent.transform);
        }
    }
    
    void HideDamageArea()
    {
        foreach (Transform child in _damageMarkerParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void WinGame()
    {
        _winCanvas.SetActive(true);
    }

    public void DefeatGame()
    {
        _defeatCanvas.SetActive(true);
    }

    public List<GameObject> GetEntitiesAtPositions(List<Vector3> positions)
    {
        List<GameObject> result = new List<GameObject>();
        Vector2 overlapSize = Vector2.one * .9f;
        foreach (var position in positions)
        {
            Collider2D collision = Physics2D.OverlapBox(position, overlapSize, 0f, _entitiesLayerMask);
            if (collision != null)
            {
                result.Add(collision.gameObject);
            }
        }

        return result;
    }

}
