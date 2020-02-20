using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cards/New Card")]
public class Card : ScriptableObject
{

    [SerializeField] protected string m_Title = default;
    public string Title { get => m_Title; protected set => m_Title = value; }

    [SerializeField] protected Sprite m_Icon = default;
    public Sprite Icon { get => m_Icon; protected set => m_Icon = value; }

    [SerializeField] protected int m_ManaCost = default;
    public int ManaCost { get => m_ManaCost; protected set => m_ManaCost = value; }

    [SerializeField] protected int m_Range = default;
    public int Range { get => m_Range; protected set => m_Range = value; }

    [SerializeField] protected BaseCardArea m_UseArea = default;
    public BaseCardArea UseArea { get => m_UseArea; protected set => m_UseArea = value; }

    [SerializeField] protected int m_AreaValue = default;
    public int AreaValue { get => m_AreaValue; protected set => m_AreaValue = value; }

    [SerializeField] protected List<BaseCardEffect> m_OnPlayEffects = default;
    public List<BaseCardEffect> OnPlayEffects { get => m_OnPlayEffects; protected set => m_OnPlayEffects = value; }

    public string Description {
        get 
        {
            string description = "";
            foreach (var item in OnPlayEffects)
            {
                if (description.Length > 0)description += ". ";
                description += item.Description;
            }

            return description;
        }
    }
    
    public void Play(Vector3 point, Vector3 casterPosition)
    {
        List<Vector3> castPosition = GetAreaOfEffect(point, casterPosition);
        foreach (BaseCardEffect effect in OnPlayEffects)
        {
            effect.OnPlay(castPosition);
        }
    }

    public List<Vector3> GetAreaOfEffect(Vector3 castPosition, Vector3 casterPosition)
    {
        return m_UseArea.GetAreaOfEffect(castPosition, casterPosition, m_AreaValue);
    }

}
