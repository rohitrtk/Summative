using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to another gameobject, spawns the chest around the map based
/// on predefined locations on the map
/// </summary>
public class ChestSpawner : MonoBehaviour {

    [SerializeField] private Transform[] _chestSpawns;  // Array of transforms
    [SerializeField] private Chest _chestPrefab;        // Chest prefab
    [HideInInspector] public List<Chest> Chests;        // List of chests

    /// <summary>
    /// Called by Unity on object creation
    /// </summary>
	private void Start ()
    {
        Chests = new List<Chest>();
        _chestSpawns = GetComponentsInChildren<Transform>();

        foreach(Transform c in _chestSpawns)
        {
            if (c.gameObject.GetInstanceID() == GetInstanceID()) continue;
            Chests.Add(Instantiate(_chestPrefab, c.position, c.rotation));
        }
	}
}