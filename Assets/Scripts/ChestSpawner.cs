using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour {

    [SerializeField] private Transform[] _chestSpawns;
    [SerializeField] private Chest _chestPrefab;
    [HideInInspector] public List<Chest> Chests;

	void Start ()
    {
        Chests = new List<Chest>();
        _chestSpawns = GetComponentsInChildren<Transform>();

        foreach(Transform c in _chestSpawns)
        {
            if (c.gameObject.GetInstanceID() == GetInstanceID()) continue;
            Chests.Add(Instantiate(_chestPrefab, c.position, c.rotation));
        }
	}

    void Update ()
    {
	}
}