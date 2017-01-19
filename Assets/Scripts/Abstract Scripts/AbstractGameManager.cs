using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AbstractGameManager : MonoBehaviour {

    // Housekeeping
    protected bool _combatPhase;    // Current phase | combat vs build
    protected int _roundNumber;     // Current round number
    protected int _numOfRounds;     // Number of rounds to win on this map

    // Enemy
    [SerializeField] protected int _numEnemies;                    // Number of enemies to spawn
    protected List<GameObject> _enemies;
    protected enum Difficulty { Easy = 1, Normal = 5, Hard = 10 }  // Difficulty level of level

    // Chests
    [SerializeField] protected int _numOfChests;        // Number of chests on map

    // Prefabs
    [SerializeField] protected GameObject _chestInstance;
    [SerializeField] protected GameObject[] _chestSpawns;

    // Other
    [SerializeField] protected float BufferTime;
    [SerializeField] protected float BuildPhaseTime;
    protected float _timer;

    public virtual void Start ()
    {
        _timer = BuildPhaseTime;
        SpawnChests();

        StartCoroutine(GameLoop());
	}
	
	public virtual void Update ()
    {
	}

    public virtual void SpawnChests()
    {
        foreach(var v in _chestSpawns)
        {

        }
    }

    #region _GAME_LOOP_

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(BuildPhase());
        yield return StartCoroutine(CombatPhase());
    }

    private IEnumerator BuildPhase()
    {
        while(!_combatPhase)
        {
            _timer -= Time.deltaTime;

            if(_timer < 0f)
            {
                _combatPhase = true;
                _timer = BuildPhaseTime;
            }

            yield return null;
        }

        yield return new WaitForSeconds(BufferTime);
    }

    private IEnumerator CombatPhase()
    {
        while(_combatPhase)
        {
            yield return null;
        }

        yield return new WaitForSeconds(BufferTime);
    }

    #endregion
}
