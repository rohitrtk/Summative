using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the spawning of enemies
/// </summary>
public class EnemySpawner : MonoBehaviour {

    [SerializeField] private AbstractEnemy _enemyPrefab;    // Abstract enemy prefab
    [SerializeField] private float _baseTime;               // Base spawn time
    [HideInInspector] public int NumberOfEnemies = 3;       // Number of enemies to be spawned this round
    public GameObject CrystalInstance;                      // Instance of crystal to target
    public Player PlayerInstance;                           // Instance of player to target
    public List<AbstractEnemy> Enemies;                     // List of enemies
    public bool CombatPhase;                                // Is there a combat phase right now?
    public bool CanSpawn;                                   // Can an enemy be spawned right now?
    public bool _waveDefeated;

    private List<Transform> _nodesList;                     // The node list attached to the spawner object
    private float _timer;                                   // Timer before next spawn
    private int _spawnCount;                                // Number of enemies that have spawned so far
    private int _deathCount;

	/// <summary>
    /// Called by Unity on object creation
    /// </summary>
	private void Awake ()
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
	private void Update ()
    {
        if (!CombatPhase) return;

        // If an enemy can be spawned, instantiate and add to the list of enemies
		if(CanSpawn && _spawnCount < NumberOfEnemies)
        {
            Enemies.Add(Instantiate(_enemyPrefab, transform.position, transform.rotation));
            Enemies[_spawnCount].SetNodeSystem(_nodesList);
            Enemies[_spawnCount].PlayerInstance = PlayerInstance;
            Enemies[_spawnCount].CrystalInstance = CrystalInstance.gameObject;
            Enemies[_spawnCount].AssignedNumber = _spawnCount;

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

        List<int> deadEnemies = new List<int>();
        foreach (AbstractEnemy e in Enemies) if (e._dead) deadEnemies.Add(e.AssignedNumber);

        foreach (int i in deadEnemies)
        {
            Destroy(Enemies[i].gameObject);
            Enemies.RemoveAt(i);
        }
    }
}