using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameCanvas : MonoBehaviour
{
    
    [SerializeField] private Button _moveButton = default;
    [SerializeField] private Button _drawPileButton = default;
    [SerializeField] private Button _discardPileButton = default;

    private GameController _gameController;
    private PlayerEntity _playerEntity;

    void Start()
    {
        _gameController = DI.Get<GameController>();
        _playerEntity = GameObject.FindObjectOfType<PlayerEntity>();
    }

    void OnEnable()
    {
        _drawPileButton.onClick.AddListener(HandleDrawPileClick);
        _discardPileButton.onClick.AddListener(HandleDiscardPileClick);
        _moveButton.onClick.AddListener(HandleMoveClick);
    }

    void OnDisable()
    {
        _drawPileButton.onClick.RemoveListener(HandleDrawPileClick);
        _discardPileButton.onClick.RemoveListener(HandleDiscardPileClick);
        _moveButton.onClick.RemoveListener(HandleMoveClick);
    }

    void HandleDrawPileClick()
    {
        _playerEntity.PlayerDeck.ShowDrawPile();
    }

    void HandleDiscardPileClick()
    {
        _playerEntity.PlayerDeck.ShowDiscardPile();
    }

    void HandleMoveClick()
    {
        _gameController.SelectMoving();
    }

}
