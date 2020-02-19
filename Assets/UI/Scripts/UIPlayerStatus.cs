using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerStatus : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _playerManaText;
    [SerializeField] private TextMeshProUGUI _playerHealthText;
    [SerializeField] private TextMeshProUGUI _playerArmorText;

    private PlayerController _playerController;

    void Awake()
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
