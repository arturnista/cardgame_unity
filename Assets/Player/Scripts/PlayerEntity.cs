using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{

    protected PlayerHealth m_PlayerHealth;
    public PlayerHealth PlayerHealth { get => m_PlayerHealth; }

    protected PlayerDeck m_PlayerDeck;
    public PlayerDeck PlayerDeck { get => m_PlayerDeck; }

    protected PlayerMovement m_PlayerMovement;
    public PlayerMovement PlayerMovement { get => m_PlayerMovement; }

    protected EntityModifiers m_EntityModifiers;
    public EntityModifiers EntityModifiers { get => m_EntityModifiers; }

    protected override void Awake()
    {
        base.Awake();
        m_PlayerHealth = GetComponent<PlayerHealth>();
        m_PlayerDeck = GetComponent<PlayerDeck>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_EntityModifiers = GetComponent<EntityModifiers>();
    }

    public override void StartTurn()
    {
        m_PlayerHealth.StartTurn();
        m_PlayerDeck.StartTurn();
        m_EntityModifiers.StartTurn();
    }

    public override void EndTurn()
    {
        m_PlayerHealth.EndTurn();
        m_PlayerDeck.EndTurn();
        m_EntityModifiers.EndTurn();
    }

}