using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIntent
{
    
    [SerializeField] protected Sprite m_Icon;
    public Sprite Icon { get => m_Icon; }

    [SerializeField] protected Color m_Color;
    public Color Color { get => m_Color; }

    [SerializeField] protected int m_Value;
    public int Value { get => m_Value; set => m_Value = value; }

    [SerializeField] protected List<Vector3> m_Positions;
    public List<Vector3> Positions { get => m_Positions; }

    public EnemyIntent(Sprite icon, Color color, List<Vector3> positions) : this(icon, color, positions, 0)
    {
        
    }

    public EnemyIntent(Sprite icon, Color color, List<Vector3> positions, int value)
    {
        m_Icon = icon;
        m_Color = color;
        m_Positions = positions;
        m_Value = value;
    }

}