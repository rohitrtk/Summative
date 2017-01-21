using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private AbstractEnemy _enemyPrefab;
    public List<AbstractEnemy> Enemies;
    private List<Transform> _nodesList;
    private Transform[] _nodes;

	// Use this for initialization
	void Start ()
    {
        _nodesList = new List<Transform>();
        
        foreach(Transform t in transform)
        {
            _nodesList.Add(t);
        }

        //_nodes = _nodesList.ToArray();
        
        Enemies = new List<AbstractEnemy>();
        Enemies.Add(Instantiate(_enemyPrefab, transform.position, transform.rotation));
        foreach(AbstractEnemy e in Enemies)
        {
            e.SetNodeSystem(_nodesList);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
