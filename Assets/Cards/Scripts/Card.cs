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

    [SerializeField] protected bool m_RequiresTarget = default;
    public bool RequiresTarget { get => m_RequiresTarget; protected set => m_RequiresTarget = value; }

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
    
    public void Play(GameObject target)
    {
        foreach (BaseCardEffect effect in OnPlayEffects)
        {
            effect.OnPlay(target);
        }
    }

}
