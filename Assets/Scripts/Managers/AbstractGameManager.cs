using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractGameManager : MonoBehaviour {

    // Housekeeping
    protected bool _combatPhase;    // Current phase | combat vs build
    protected int _roundNumber;     // Current round number
    protected int _numOfRounds;     // Number of rounds to win on this map

    // Enemy
    [SerializeField] protected int _numEnemies;                    // Number of enemies to spawn
    protected enum Difficulty { Easy = 1, Normal = 5, Hard = 10 }  // Difficulty level of level

    // Chests
    [SerializeField] protected int _numOfChests;        // Number of chests on map

    // Prefabs
    [SerializeField] protected GameObject _chestInstance;
    [SerializeField] protected GameObject[] _chestSpawns;

    public virtual void Start ()
    {
        SpawnChests();
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
}
