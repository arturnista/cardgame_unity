using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    
    [SerializeField] private Encounter _encounter = default;
    [SerializeField] private List<Transform> _spawnPositions = default;
    
    private GameController _gameController;

    private GameObject _enemiesParent;

    private List<GameObject> m_Enemies;
    public List<GameObject> Enemies { get => m_Enemies; }

    void Awake()
    {
        DI.Set<EnemiesController>(this);
        _gameController = GameObject.FindObjectOfType<GameController>();
    }

    public void StartGame()
    {
        m_Enemies = new List<GameObject>();
        
        _enemiesParent = new GameObject("Enemies");
        for (int i = 0; i < _encounter.EnemiesPrefabs.Count; i++)
        {
            GameObject enemyPrefab = _encounter.EnemiesPrefabs[i];
            GameObject createdEnemy = Instantiate(enemyPrefab, _spawnPositions[i].position, Quaternion.identity, _enemiesParent.transform) as GameObject;
            m_Enemies.Add(createdEnemy);

            createdEnemy.GetComponent<EnemyHealth>().OnDeath += HandleEnemyDeath;
        }
        
    }

    void HandleEnemyDeath(GameObject enemy)
    {
        m_Enemies.Remove(enemy);
    }

    public GameObject GetEnemy(int index)
    {
        return m_Enemies[index];
    }

    public void StartTurn()
    {
        PlayerController playerController = GameObject.FindObjectOfType<PlayerController>();
        for (int i = m_Enemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = m_Enemies[i];
            enemy.GetComponent<EntityModifiers>().StartTurn();

        }
        for (int i = m_Enemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = m_Enemies[i];
            enemy.GetComponent<EnemyAI>().Execute(playerController, this, enemy);
        }

        _gameController.EndEnemiesTurn();
    }

    public void EndTurn()
    {

        for (int i = m_Enemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = m_Enemies[i];
            enemy.GetComponent<EntityModifiers>().EndTurn();
        }

        for (int i = m_Enemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = m_Enemies[i];
            enemy.GetComponent<UIEnemy>().UpdateUI();
        }

    }
    
}
