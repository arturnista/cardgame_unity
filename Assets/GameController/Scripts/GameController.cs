using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    
    [SerializeField] private GameObject _playerEntityPrefab = default;
    [SerializeField] private GameObject _damageMarkerPrefab = default;
    [SerializeField] private GameObject _damageMarkerParent = default;
    [SerializeField] private LayerMask _entitiesLayerMask = default;
    [Header("UI")]
    [SerializeField] private GameObject _cardTemplate = default;
    [SerializeField] private GameObject _cardParent = default;

    private PlayerController _playerController;
    private EnemiesController _enemiesController;
    private MapController _mapController;

    private Camera _camera;

    private Card m_SelectedCard;
    public Card SelectedCard { get => m_SelectedCard; }

    private bool _selectedCardThisFrame = false;
    private Vector3 _lastMousePosition;

    void Awake()
    {
        DI.Set<GameController>(this);
        
        GameObject playerCreated = Instantiate(_playerEntityPrefab, new Vector3(.5f, -.5f), Quaternion.identity);
        _playerController = playerCreated.GetComponent<PlayerController>();

        _camera = Camera.main;
    }

    void Start()
    {
        _enemiesController = GameObject.FindObjectOfType<EnemiesController>();
        _mapController = DI.Get<MapController>();
        
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

        if (!_selectedCardThisFrame && Input.GetKeyDown(KeyCode.Mouse0) && m_SelectedCard != null)
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            SelectPoint(_mapController.WorldToCell(mousePosition));
        }
    }

    void LateUpdate()
    {
        _selectedCardThisFrame = false;

        if (SelectedCard != null)
        {
            DrawDamageArea();
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
        if (card.ManaCost > _playerController.ManaAmount)
        {
            DebugText.ShowText("NOT ENOUGH MANA");
            return;
        }
        m_SelectedCard = card;
        _selectedCardThisFrame = true;
        DrawDamageArea(true);
    }

    public void SelectPoint(Vector3 point)
    {
        if (m_SelectedCard != null)
        {
            _playerController.PlayCard(m_SelectedCard, point);
            m_SelectedCard = null;
            HideDamageArea();
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

    void DrawDamageArea(bool force = false)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = _mapController.WorldToCell(mousePosition);
        if (!force && _lastMousePosition == mousePosition)
        {
            return;
        }
        _lastMousePosition = mousePosition;

        HideDamageArea();
        foreach (var pos in SelectedCard.GetAreaOfEffect(mousePosition, _playerController.transform.position))
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

    void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.color = Color.red;
            MapController mapController = DI.Get<MapController>();
            if (SelectedCard != null)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                foreach (var pos in SelectedCard.GetAreaOfEffect(mapController.WorldToCell(mousePosition), _playerController.transform.position))
                {
                    Gizmos.DrawWireCube(pos, Vector3.one * .7f);
                }
            }
        }

    }

}
