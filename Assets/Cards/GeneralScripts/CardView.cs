using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    [Header("UI")]
    [SerializeField] private SpriteRenderer _iconSpriteRenderer = default;
    [SerializeField] private TextMeshPro _titleText = default;
    [SerializeField] private TextMeshPro _descriptionText = default;
    [SerializeField] private TextMeshPro _manaCostText = default;

    private BaseCard _card;
    private int _cardIndex;

    private RectTransform _rectTransform;

    private GameController _gameController;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Construct(BaseCard card, int index, GameController gameController)
    {
        _card = card;
        _cardIndex = index;
        _gameController = gameController;

        _iconSpriteRenderer.sprite = _card.Icon;
        _titleText.text = _card.Title;
        _descriptionText.text = _card.Description;
        _manaCostText.text = _card.ManaCost.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        _rectTransform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        _rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("OnPointerDown " + _cardIndex);
        // _gameController.PlayCard(_cardIndex);
    }

}
