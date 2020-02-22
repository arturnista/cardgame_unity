using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTypeHolder : MonoBehaviour
{

    [SerializeField] protected List<EntityType> m_Types;
    public List<EntityType> Types { get => m_Types; }

    public bool IsType(List<EntityType> types)
    {
        foreach (var type in types)
        {
            bool isType = IsType(type);
            if (isType) return true;
        }
        
        return false;
    }

    public bool IsType(EntityType type)
    {
        return m_Types.Contains(type);
    }

}
