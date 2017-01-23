using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/// <summary>
/// The abstract layer of game managers
/// </summary>
public abstract class AbstractGameManager : MonoBehaviour {

    // States
    protected enum State { Menu, Game , Setup, Options }
    protected State _gameState;

    // Players
    [SerializeField] protected Player _player;                     // Player prefab
    [SerializeField] protected Transform _playerSpawn;             // Players spawn
    protected Player _playerInstance;                              // Instance of the player to spawn

    // Housekeeping
    protected bool _combatPhase;                                   // Current phase | combat vs build
    protected int _roundNumber;                                    // Current round number
    protected int _numOfRounds;                                    // Number of rounds to win on this map

    // Enemy
    [SerializeField] protected int _numEnemies;                    // Number of enemies to spawn
    protected List<GameObject> _enemies;                           // List of enemies to be spawned
    protected enum Difficulty { Easy = 1, Normal = 5, Hard = 10 }  // Difficulty level of level
    protected EnemySpawner _es;                                    // Enemy spawner reference

    // Chests
    [SerializeField] protected int _numOfChests;                    // Number of chests on map

    // Prefabs
    [SerializeField] protected GameObject _crystalPrefab;
    [SerializeField] protected Transform _crystalSpawn;             // Spawn point for the crystal
                     protected GameObject _crystalInstance;         // Instance of the crystal
    [SerializeField] protected GameObject _chestInstance;           // Instance of the chest
    [SerializeField] protected GameObject[] _chestSpawns;           // Array of spawns for chest

    // Other
    [SerializeField] protected float BufferTime;                    // Time it takes to switch between phases
    [SerializeField] protected float BuildPhaseTime;                // Time before build phase and/or combat phase to end
    protected float _timer;                                         // Timer based on Time.deltaTime

    /// <summary>
    /// Called by Unity for object init
    /// </summary>
    public virtual void Start ()
    {
        _gameState = State.Menu;

        // Start the game loop
        StartCoroutine(GameLoop());
	}
	
    // Called once per frame
	public virtual void Update ()
    {
	}

    #region _GAME_LOOP_

    /// <summary>
    /// Recursive method, loops through the build phase, combat phase and states
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameLoop()
    {
        if(_gameState == State.Menu || _gameState == State.Setup)
        {
            yield return StartCoroutine(Menu());
            yield return StartCoroutine(Launch());
        }
        else
        {
            yield return StartCoroutine(BuildPhase());
            yield return StartCoroutine(CombatPhase());
        }

        // Infinite game loop
        StartCoroutine(GameLoop());
    }

    private IEnumerator Menu()
    {
        while(_gameState == State.Menu)
        {
            yield return null;
        }

        GameObject.Find("HUD").SetActive(false);

        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator Launch()
    {
        // Set player up
        _playerInstance = Instantiate(_player, _playerSpawn.position, _playerSpawn.rotation);
        GameObject.Find("Main Camera").GetComponent<Camera>().gameObject.SetActive(false);
        _playerInstance.GetComponentInChildren<Camera>().enabled = true;

        // Set crystal up
        _crystalInstance = Instantiate(_crystalPrefab, _crystalSpawn.position, _crystalSpawn.rotation);

        // Set enemies up
        _es = GetComponentInChildren<EnemySpawner>();
        _es.enabled = true;
        _es.CrystalInstance = _crystalInstance;
        _es.PlayerInstance = _playerInstance;

        // Set up timer
        _timer = BuildPhaseTime;
        _roundNumber = 0;

        _gameState = State.Game;

        yield return new WaitForSeconds(0.1f);
    }

    /// <summary>
    /// Method is called to run build phase specific things
    /// </summary>
    /// <returns></returns>
    private IEnumerator BuildPhase()
    {
        // While it is the build phase
        while (!_combatPhase)
        {
            Timer();

            yield return null; // Don't leave method
        }

        ++_roundNumber;        // Add one to round number before leaving method

        yield return new WaitForSeconds(BufferTime);
    }

    /// <summary>
    /// Method is called to run combat phase specific things
    /// </summary>
    /// <returns></returns>
    private IEnumerator CombatPhase()
    {
        // While it is the combat phase
        while (_combatPhase)
        {
            Timer();

            yield return null;
        }

        yield return new WaitForSeconds(BufferTime); // Don't leave
    }

    #endregion

    /// <summary>
    /// Called to run a countdown based on Time.deltaTime
    /// </summary>
    private void Timer()
    {
        _timer -= Time.deltaTime;                       // -1 per second

        if (_timer < 0.1f)
        {
            // Set phases
            if (_combatPhase) _combatPhase = false;      
            else if(!_combatPhase)_combatPhase = true;

            _timer = BuildPhaseTime;                    // Reset timer
        }
    }

    /// <summary>
    /// Gets the current round in the game manager
    /// </summary>
    /// <returns></returns>
    public int GetRound()
    {
        return _roundNumber;
    }

    // Gets the player prefab
    public Player GetPlayer()
    {
        return _player;
    }
}