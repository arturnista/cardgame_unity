using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffectItem
{

    public BaseCardEffect Effect;
    public int[] Values;

}

[CreateAssetMenu(menuName="Cards/New Card")]
public class Card : ScriptableObject
{

    [SerializeField] protected string m_ID = default;
    public string ID { get => m_ID; protected set => m_ID = value; }

    [SerializeField] protected string m_Title = default;
    public string Title { get => m_Title; protected set => m_Title = value; }

    [SerializeField] protected Sprite m_Icon = default;
    public Sprite Icon { get => m_Icon; protected set => m_Icon = value; }

    [SerializeField] protected int m_ManaCost = default;
    public int ManaCost { get => m_ManaCost; protected set => m_ManaCost = value; }

    [Header("Cast area")]
    [SerializeField] protected int m_CastRange = default;
    public int CastRange { get => m_CastRange; protected set => m_CastRange = value; }

    [SerializeField] protected BaseAreaOfEffect m_CastArea = default;
    public BaseAreaOfEffect CastArea { get => m_CastArea; protected set => m_CastArea = value; }

    [SerializeField] protected int m_CastAreaSize = default;
    public int CastAreaSize { get => m_CastAreaSize; protected set => m_CastAreaSize = value; }

    [Header("Effects")]
    [Tooltip("OnPlay Card Effects are actions that the card will do when Played")]
    [SerializeField] protected List<CardEffectItem> m_OnPlayEffects = default;
    public List<CardEffectItem> OnPlayEffects { get => m_OnPlayEffects; protected set => m_OnPlayEffects = value; }

    public string Description {
        get 
        {
            string description = "";
            foreach (var item in OnPlayEffects)
            {
                if (description.Length > 0) description += ". ";
                description += item.Effect.GetDescription(item.Values);
            }

            return description;
        }
    }
    
    public void Play(Vector3 point, Vector3 casterPosition)
    {
        List<Vector3> castPosition = GetAreaOfEffect(point, casterPosition);
        foreach (CardEffectItem item in OnPlayEffects)
        {
            item.Effect.OnPlay(castPosition, item.Values);
        }
    }

    public List<Vector3> GetAreaOfEffect(Vector3 castPosition, Vector3 casterPosition)
    {
        return m_CastArea.GetAreaOfEffect(castPosition, casterPosition, m_CastAreaSize);
    }

}
