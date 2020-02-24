using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIEnemy : UIEntity
{

    [Header("AI")]
    [SerializeField] private Image _intentIcon = default;
    [SerializeField] private TextMeshProUGUI _intentTextValue = default;
    [SerializeField] private GameObject _damageMarkerPrefab = default;
    [SerializeField] private GameObject _damageMarkerParent = default;

    private BaseEnemyAI _enemyAI;

    protected override void Awake()
    {
        base.Awake();
        _enemyAI = GetComponent<BaseEnemyAI>();
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        UpdateIntent();
    }

    void UpdateIntent()
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

        foreach (Transform child in _damageMarkerParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Vector3 pos in intent.Positions)
        {
            Instantiate(_damageMarkerPrefab, pos, Quaternion.identity, _damageMarkerParent.transform);
        }
    }

}
