using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIEntity : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] protected TextMeshProUGUI _healthTextValue = default;
    [SerializeField] protected Image _healthBar = default;
    [SerializeField] protected Canvas _armorCanvas = default;
    [SerializeField] protected TextMeshProUGUI _armorTextValue = default;

    [Header("Health")]
    [SerializeField] protected Canvas _modifiersCanvas = default;
    [SerializeField] protected GameObject _modifierTemplate = default;

    protected EntityHealth _entityHealth;
    protected EntityModifiers _entityModifiers;

    protected virtual void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();
        _entityModifiers = GetComponent<EntityModifiers>();
    }

    protected virtual void Start()
    {
        UpdateHealth();
    }

    protected virtual void OnEnable()
    {
        _entityHealth.OnDamage += UpdateHealth;
        _entityModifiers.OnChangeModifiers += UpdateModifiers;
    }
    
    protected virtual void OnDisable()
    {
        _entityHealth.OnDamage -= UpdateHealth;
        _entityModifiers.OnChangeModifiers -= UpdateModifiers;
    }

    public virtual void UpdateUI()
    {
        UpdateHealth();
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

    void UpdateModifiers(GameObject enemy = null)
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
