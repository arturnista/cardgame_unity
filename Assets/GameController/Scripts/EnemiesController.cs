using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{

    public delegate void DefeatAllEnemiesHandler();
    public DefeatAllEnemiesHandler OnDefeatAllEnemies;
    
    [SerializeField] private Encounter _encounter = default;
    [SerializeField] private List<Transform> _spawnPositions = default;
    
    private EncounterManager _encounterManager;
    private GameController _gameController;

    private GameObject _enemiesParent;

    private List<GameObject> m_Enemies;
    public List<GameObject> Enemies { get => m_Enemies; }

    void Awake()
    {
        DI.Set<EnemiesController>(this);
        _gameController = GameObject.FindObjectOfType<GameController>();

        _encounterManager = DI.Get<EncounterManager>();
        if (_encounterManager != null && _encounterManager.LoadedEncounter != null)
        {
            _encounter = _encounterManager.LoadedEncounter;
        }
    }

    public void CreateEnemies()
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

    public void StartGame()
    {
    }

    void HandleEnemyDeath(GameObject enemy)
    {
        m_Enemies.Remove(enemy);
        if (m_Enemies.Count == 0)
        {
            if (OnDefeatAllEnemies != null)
            {
                OnDefeatAllEnemies();
            }
        }
    }

    public GameObject GetEnemy(int index)
    {
        return m_Enemies[index];
    }

    public void StartTurn()
    {
        for (int i = m_Enemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = m_Enemies[i];
            enemy.GetComponent<EntityModifiers>().StartTurn();

        }
        
        PlayerEntity playerEntity = GameObject.FindObjectOfType<PlayerEntity>();
        for (int i = m_Enemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = m_Enemies[i];
            enemy.GetComponent<BaseEnemyAI>().Execute();
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

        PlayerEntity playerEntity = GameObject.FindObjectOfType<PlayerEntity>();
        for (int i = m_Enemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = m_Enemies[i];
            enemy.GetComponent<BaseEnemyAI>().PrepareNextAction();
            enemy.GetComponent<UIEnemy>().UpdateUI();
        }

    }
    
}
