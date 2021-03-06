﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class UICardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    [Header("UI")]
    [SerializeField] private Image _iconImage = default;
    [SerializeField] private TextMeshProUGUI _titleText = default;
    [SerializeField] private TextMeshProUGUI _descriptionText = default;
    [SerializeField] private TextMeshProUGUI _manaCostText = default;
    [SerializeField] private GameObject _selectBorder = default;

    private Vector3 _originalSize;

    private bool _isInteractable;

    private BaseCard _card;
    private int _cardIndex;

    private RectTransform _rectTransform;

    private GameController _gameController;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _selectBorder.SetActive(false);
        _originalSize = _rectTransform.localScale;
    }

    public void Construct(BaseCard card)
    {
        _card = card;
        _isInteractable = false;
        UpdateUI();
    }

    public void Construct(BaseCard card, int index, GameController gameController)
    {
        _card = card;
        _cardIndex = index;
        _gameController = gameController;
        _isInteractable = true;
        UpdateUI();
    }

    void UpdateUI()
    {
        _iconImage.sprite = _card.Icon;
        _titleText.text = _card.Title;
        _descriptionText.text = _card.Description;
        _manaCostText.text = _card.ManaCost.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isInteractable) return;
        _rectTransform.localScale = _originalSize * 1.5f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isInteractable) return;
        _rectTransform.localScale = _originalSize;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (!_isInteractable) return;
        _gameController.SelectCard(_cardIndex, _card, this);
    }
    
    public void Select()
    {
        _selectBorder.SetActive(true);
    }

    public void Deselect()
    {
        _selectBorder.SetActive(false);
    }

}
