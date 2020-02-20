using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIEnemy : MonoBehaviour
{

    [Header("AI")]
    [SerializeField] private Image _intentIcon = default;
    [SerializeField] private TextMeshProUGUI _intentTextValue = default;

    [Header("Health")]
    [SerializeField] protected TextMeshProUGUI _healthTextValue = default;
    [SerializeField] protected Image _healthBar = default;
    [SerializeField] protected Canvas _armorCanvas = default;
    [SerializeField] protected TextMeshProUGUI _armorTextValue = default;

    [Header("Health")]
    [SerializeField] protected Canvas _modifiersCanvas = default;
    [SerializeField] protected GameObject _modifierTemplate = default;

    private EnemyAI _enemyAI;
    private EntityHealth _entityHealth;
    private EntityModifiers _entityModifiers;

    void Awake()
    {
        _enemyAI = GetComponent<EnemyAI>();
        _entityHealth = GetComponent<EntityHealth>();
        _entityModifiers = GetComponent<EntityModifiers>();
    }

    void Start()
    {
        UpdateHealth();
    }

    void OnEnable()
    {
        _entityHealth.OnDamage += UpdateHealth;
        _entityModifiers.OnChangeModifiers += UpdateModifiers;
    }
    
    void OnDisable()
    {
        _entityHealth.OnDamage -= UpdateHealth;
        _entityModifiers.OnChangeModifiers -= UpdateModifiers;
    }

    public void UpdateUI()
    {
        UpdateHealth();
        UpdateIntent();
        UpdateModifiers();
    }

    void UpdateHealth(GameObject enemy = null)
    {
        float healthToShow = Mathf.Clamp(_entityHealth.Health, 0, _entityHealth.MaxHealth);
        _healthBar.fillAmount = healthToShow / _entityHealth.MaxHealth;
        _healthTextValue.text = healthToShow + "/" + _entityHealth.MaxHealth;

        if (_entityHealth.Armor > 0)
        {
            _armorCanvas.enabled = true;
            _armorTextValue.text = _entityHealth.Armor.ToString();
        }
        else
        {
            _armorCanvas.enabled = false;
        }
    }

    public void UpdateIntent()
    {
        EnemyIntent intent = _enemyAI.NextAction.GetIntent();
        _intentIcon.sprite = intent.Icon;
        _intentIcon.color = intent.Color;
        if (intent.Value > 0)
        {
            _intentTextValue.text = intent.Value.ToString();
        }
        else
        {
            _intentTextValue.text = "";
        }
    }

    public void UpdateModifiers(GameObject enemy = null)
    {
        foreach (Transform child in _modifiersCanvas.transform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var modifier in _entityModifiers.Modifiers)
        {
            GameObject created = Instantiate(_modifierTemplate,  _modifiersCanvas.transform);
            Image ui = created.GetComponentInChildren<Image>();
            ui.sprite = modifier.Value.Icon;
            TextMeshProUGUI textMesh = created.GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = modifier.Value.Amount.ToString();
        }
    }

}
