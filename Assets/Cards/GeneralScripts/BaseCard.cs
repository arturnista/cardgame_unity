using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard : ScriptableObject
{

    [SerializeField] protected string m_ID = "card:";
    public string ID { get => m_ID; protected set => m_ID = value; }

    [SerializeField] protected string m_Title = default;
    public string Title { get => m_Title; protected set => m_Title = value; }

    [SerializeField] protected Sprite m_Icon = default;
    public Sprite Icon { get => m_Icon; protected set => m_Icon = value; }

    [SerializeField] protected int m_ManaCost = default;
    public int ManaCost { get => m_ManaCost; protected set => m_ManaCost = value; }

    [Header("Cast area")]
    [SerializeField] protected List<EntityType> m_CastLayer = default;
    public List<EntityType> CastLayer { get => m_CastLayer; protected set => m_CastLayer = value; }

    [SerializeField] protected int m_CastRange = default;
    public int CastRange { get => m_CastRange; protected set => m_CastRange = value; }

    [SerializeField] protected BaseAreaOfEffect m_CastArea = default;
    public BaseAreaOfEffect CastArea { get => m_CastArea; protected set => m_CastArea = value; }

    [SerializeField] protected int m_CastAreaSize = default;
    public int CastAreaSize { get => m_CastAreaSize; protected set => m_CastAreaSize = value; }
    
    protected List<BaseCardEffect> m_OnPlayEffects = default;
    public List<BaseCardEffect> OnPlayEffects { get => m_OnPlayEffects; protected set => m_OnPlayEffects = value; }

    protected PlayerEntity _playerEntity;

    public string Description {
        get 
        {
            string description = "";
            foreach (var item in OnPlayEffects)
            {
                description += item.GetDescription() + "\n";
            }

            PlayerDeck.DeckPiles destination = GetDestination();
            switch (destination)
            {
                case PlayerDeck.DeckPiles.Exaust:
                    description += "Exaust";
                    break;
                case PlayerDeck.DeckPiles.Hand:
                    description += "Move to hand";
                    break;
                case PlayerDeck.DeckPiles.Draw:
                    description += "Move to draw pile";
                    break;
            }

            return description;
        }
    }

    public void Construct(PlayerEntity playerEntity)
    {
        _playerEntity = playerEntity;
    }

    public abstract void Initialize();
    
    public PlayerDeck.DeckPiles Play(Vector3 point, Vector3 casterPosition)
    {
        List<Vector3> castPosition = GetAreaOfEffect(point, casterPosition);
        foreach (BaseCardEffect item in OnPlayEffects)
        {
            item.OnPlay(this, castPosition);
        }
        return GetDestination();
    }

    protected virtual PlayerDeck.DeckPiles GetDestination()
    {
        return PlayerDeck.DeckPiles.Discard;
    }

    public List<Vector3> GetAreaOfEffect(Vector3 castPosition, Vector3 casterPosition)
    {
        return m_CastArea.GetAreaOfEffect(castPosition, casterPosition, m_CastAreaSize);
    }

    public void Discard()
    {
        _playerEntity.PlayerDeck.MoveCard(this, PlayerDeck.DeckPiles.Discard);   
    }

    public void Exaust()
    {
        _playerEntity.PlayerDeck.MoveCard(this, PlayerDeck.DeckPiles.Exaust);
    }

}
