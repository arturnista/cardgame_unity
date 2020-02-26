using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Encounters/New encounter")]
public class Encounter : ScriptableObject
{
    
    [SerializeField] private string m_ID = "encounter:";
    public string ID { get => m_ID; }

    [SerializeField] private List<GameObject> m_EnemiesPrefabs = default;
    public List<GameObject> EnemiesPrefabs { get => m_EnemiesPrefabs; }

}
