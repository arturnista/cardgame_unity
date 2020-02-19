using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICardList : MonoBehaviour
{

    public static UICardList Main;

    [SerializeField] private GameObject _cardPrefab;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private Button _backButton;
    [SerializeField] private Canvas _contentCanvas;
    [SerializeField] private Canvas _emptyContentCanvas;

    private KeyCode _backHotkey;

    void Start()
    {
        Main = this;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        _backButton.onClick.AddListener(HandleBack);
    }

    void OnDisable()
    {
        _backButton.onClick.RemoveListener(HandleBack);
        foreach (Transform child in _contentCanvas.transform)
        {
            Destroy(child.gameObject);
        }
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
        gameObject.SetActive(false);
    }

    public void Show(string title, List<Card> cards, KeyCode backHotkey)
    {
        _backHotkey = backHotkey;
        _titleText.text = title;
        gameObject.SetActive(true);
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

}
