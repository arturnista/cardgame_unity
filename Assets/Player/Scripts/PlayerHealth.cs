using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : EntityHealth
{

    private int m_ManaAmount;
    public int ManaAmount { get => m_ManaAmount; set => m_ManaAmount = value; }
    
    private GameController _gameController;

    void Awake()
    {
        _gameController = GameObject.FindObjectOfType<GameController>();

        m_Health = m_MaxHealth;
    }

    public void StartGame()
    {
        
    }

    public void StartTurn()
    {
        m_ManaAmount = 3;
        
    }

    public void EndTurn()
    {
        
    }

}
