using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICardList : MonoBehaviour
{

    public static UICardList Main;

    [SerializeField] private GameObject _cardPrefab = default;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _titleText = default;
    [SerializeField] private Button _backButton = default;
    [SerializeField] private Canvas _contentCanvas = default;
    [SerializeField] private Canvas _emptyContentCanvas = default;

    private KeyCode _backHotkey;
    private Canvas _canvas;
    private GraphicRaycaster _graphicRaycaster;

    void Start()
    {
        Main = this;
        _canvas = GetComponent<Canvas>();
        _graphicRaycaster = GetComponent<GraphicRaycaster>();
        Hide();
    }

    void Update()
    {
        if (Input.GetKeyDown(_backHotkey))
        {
            HandleBack();
        }
    }

    void HandleBack()
    {
        Hide();
    }

    public void Show(string title, List<Card> cards, KeyCode backHotkey)
    {
        _backHotkey = backHotkey;
        _titleText.text = title;
        
        Display();

        if (cards.Count > 0) 
        {
            _emptyContentCanvas.gameObject.SetActive(false);
            foreach (Card card in cards)
            {
                UICardView cardView = Instantiate(_cardPrefab, _contentCanvas.transform).GetComponent<UICardView>();
                cardView.Construct(card);
            }

        }
        else
        {
            _emptyContentCanvas.gameObject.SetActive(true);
        }
    }

    void Display()
    {
        foreach (Transform child in _contentCanvas.transform)
        {
            Destroy(child.gameObject);
        }

        gameObject.SetActive(true);
        // _canvas.enabled = true;
        // _graphicRaycaster.enabled = true;

        _backButton.onClick.AddListener(HandleBack);
    }

    void Hide()
    {       
        gameObject.SetActive(false);
        // _canvas.enabled = false;
        // _graphicRaycaster.enabled = false;

        _backButton.onClick.RemoveListener(HandleBack);
    }

}
