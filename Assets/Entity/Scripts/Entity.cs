using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    
    protected IHealth m_Health;
    public IHealth Health { get => m_Health; }

    protected IModifiersHolder m_ModifiersHolder;
    public IModifiersHolder ModifiersHolder { get => m_ModifiersHolder; }

    void Awake()
    {
        m_Health = GetComponent<IHealth>();
        m_ModifiersHolder = GetComponent<IModifiersHolder>();
    }

}
