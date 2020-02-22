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

    private PlayerEntity _playerEntity;

    void Start()
    {
        _playerEntity = GameObject.FindObjectOfType<PlayerEntity>();
    }

    void Update()
    {
        _playerManaText.text = _playerEntity.PlayerHealth.ManaAmount.ToString();
        _playerHealthText.text = _playerEntity.PlayerHealth.Health + "/" + _playerEntity.PlayerHealth.MaxHealth;
        _playerArmorText.text = _playerEntity.PlayerHealth.Armor.ToString();
    }

}
