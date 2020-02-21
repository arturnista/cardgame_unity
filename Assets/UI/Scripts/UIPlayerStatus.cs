using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerStatus : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _playerManaText = default;
    [SerializeField] private TextMeshProUGUI _playerHealthText = default;
    [SerializeField] private TextMeshProUGUI _playerArmorText = default;

    private PlayerController _playerController;

    void Start()
    {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        _playerManaText.text = _playerController.ManaAmount.ToString();
        _playerHealthText.text = _playerController.Health + "/" + _playerController.MaxHealth;
        _playerArmorText.text = _playerController.Armor.ToString();
    }

}
