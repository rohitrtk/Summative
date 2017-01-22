using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private AbstractEnemy _enemyPrefab;
    public List<AbstractEnemy> Enemies;
    private List<Transform> _nodesList;
    private Transform[] _nodes;
    private float _timer = 5f;
    public bool CanSpawn;
    public int numberOfEnemies = 3;
    private int _spawnCount;

	// Use this for initialization
	void Start ()
    {
        _nodesList = new List<Transform>();
        
        foreach(Transform t in transform)
        {
            _nodesList.Add(t);
        }

        CanSpawn = true;
        Enemies = new List<AbstractEnemy>();
        _spawnCount = 0;
        //foreach(AbstractEnemy e in Enemies)
        //{
        //   e.SetNodeSystem(_nodesList);
        //}
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(CanSpawn)
        {
            Enemies.Add(Instantiate(_enemyPrefab, transform.position, transform.rotation));
            Enemies[_spawnCount].SetNodeSystem(_nodesList);
            _spawnCount++;
            CanSpawn = false;
            return;
        }

        _timer -= Time.deltaTime;
        if(_timer <= 0f)
        {
            _timer = 5f;
            CanSpawn = true;
        }
	}
}
