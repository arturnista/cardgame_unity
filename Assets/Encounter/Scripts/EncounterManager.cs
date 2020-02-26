using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager
{
    
    private Dictionary<string, Encounter> _encounter = default;

    private Encounter m_LoadedEncounter;
    public Encounter LoadedEncounter { get => m_LoadedEncounter; }

    public EncounterManager()
    {
        _encounter = new Dictionary<string, Encounter>();
    }

    public void AddEncounter(Encounter encounter)
    {
        _encounter.Add(encounter.ID, encounter);
    }

    public void LoadEncounter(string encounterID)
    {
        m_LoadedEncounter = _encounter[encounterID];
        SceneManager.LoadScene("Encounter");
    }

}
