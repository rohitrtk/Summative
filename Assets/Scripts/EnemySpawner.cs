using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the spawning of enemies
/// </summary>
public class EnemySpawner : MonoBehaviour {

    public GameObject CrystalInstance;
    public Player PlayerInstance;
    [SerializeField] private AbstractEnemy _enemyPrefab;    // Abstract enemy prefab
    public List<AbstractEnemy> Enemies;                     // List of enemies
    private List<Transform> _nodesList;                     // The node list attached to the spawner object
    [SerializeField] private float _baseTime;               // Base spawn time
    private float _timer;                                   // Timer before next spawn
    public bool CanSpawn;                                   // Can an enemy be spawned right now?
    public int NumberOfEnemies = 3;                         // Number of enemies to be spawned this round
    private int _spawnCount;                                // Number of enemies that have spawned so far
    public bool _waveDefeated;

	/// <summary>
    /// Called by Unity on object creation
    /// </summary>
	void Awake ()
    {
        _timer = _baseTime;
        _nodesList = new List<Transform>();
        
        foreach(Transform t in transform)
        {
            _nodesList.Add(t);
        }

        CanSpawn = false;
        Enemies = new List<AbstractEnemy>();
        _spawnCount = 0;
    }
	
	/// <summary>
    /// Called by Unity once per frame
    /// </summary>
	void Update ()
    {
        // If an enemy can be spawned, instantiate and add to the list of enemies
		if(CanSpawn && !(_spawnCount >= NumberOfEnemies))
        {
            Enemies.Add(Instantiate(_enemyPrefab, transform.position, transform.rotation));
            Enemies[_spawnCount].SetNodeSystem(_nodesList);
            Enemies[_spawnCount].PlayerInstance = PlayerInstance;
            Enemies[_spawnCount].CrystalInstance = CrystalInstance.gameObject;
            
            _spawnCount++;
            CanSpawn = false;
            return;
        }
        else if(!CanSpawn)
        {
            // Timer to check when the next enemy can be spawned
            _timer -= Time.deltaTime;
            if(_timer <= 0f)
            {
                _timer = _baseTime;
                CanSpawn = true;
            }
            return;
        }

	}
}