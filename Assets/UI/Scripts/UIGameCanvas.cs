using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameCanvas : MonoBehaviour
{
    
    [SerializeField] private Button _drawPileButton = default;
    [SerializeField] private Button _discardPileButton = default;

    private PlayerEntity _playerEntity;

    void Start()
    {
        _playerEntity = GameObject.FindObjectOfType<PlayerEntity>();
    }

    void OnEnable()
    {
        _drawPileButton.onClick.AddListener(HandleDrawPileClick);
        _discardPileButton.onClick.AddListener(HandleDiscardPileClick);
    }

    void OnDisable()
    {
        _drawPileButton.onClick.RemoveListener(HandleDrawPileClick);
        _discardPileButton.onClick.RemoveListener(HandleDiscardPileClick);
    }

    void HandleDrawPileClick()
    {
        _playerEntity.PlayerDeck.ShowDrawPile();
    }

    void HandleDiscardPileClick()
    {
        _playerEntity.PlayerDeck.ShowDiscardPile();
    }

}
