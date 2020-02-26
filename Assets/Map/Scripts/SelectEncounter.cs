using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectEncounter : MonoBehaviour
{

    [SerializeField] private string _encounterID = "encounter:";

    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleButtonClick);
    }

    void HandleButtonClick()
    {
        DI.Get<EncounterManager>().LoadEncounter(_encounterID);
    }

    public void Enable()
    {
        _button.interactable = true;
    }

    public void Disable()
    {
        _button.interactable = false;
    }

}
