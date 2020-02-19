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
    public int Value { get => m_Value; }

    public EnemyIntent(Sprite icon, Color color) : this(icon, color, 0)
    {
        
    }

    public EnemyIntent(Sprite icon, Color color, int value)
    {
        m_Icon = icon;
        m_Color = color;
        m_Value = value;
    }

}