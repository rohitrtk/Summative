using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractEnemy : MonoBehaviour {

    [SerializeField] private Transform _target;     // Target destination
    private NavMeshAgent _nma;                      // Navmeshagent component

    private List<Transform> _nodes;                 // List of nodes                 
    private int _currentNode;                       // Last known node to stand on
    private int _goToNode;                          // Go to this node

    private List<Transform> _openNodes;
    private bool _canMove;

	public virtual void Start ()
    {
        _openNodes = _nodes;
        _canMove = false;
        _currentNode = -1;
        _nma = GetComponent<NavMeshAgent>();
	}

    public virtual void SetNodeSystem(List<Transform> nodes)
    {
        _nodes = nodes;
    }

    public virtual void Update ()
    {
        if (_target.position == null) return;
        Path();
	}

    public void SetTargetTransform(Transform t)
    {
        _target = t;
    }

    // Work on Path() an FindNode()
    private void Path()
    {
        if(_canMove)
        {
            _target = _nodes[_goToNode];
            _nma.SetDestination(_target.position);

            if (Vector3.Distance(_target.position, transform.position) < 2)
            {
                _canMove = false;
                _currentNode = _goToNode;
                _openNodes.RemoveAt(_goToNode);
            }

            return;
        }

        FindNode();
    }

    private void FindNode()
    {
        var distances = new Dictionary<int, float>();
        for (int i = 0; i < _openNodes.Count; i++)
        {
            distances.Add(i, Vector3.Distance(transform.position, _openNodes[i].position));
        }

        List<float> fDistances = new List<float>();
        foreach (KeyValuePair<int, float> entry in distances)
        {
            fDistances.Add(entry.Value);
        }

        Array.Sort(fDistances.ToArray());

        foreach (KeyValuePair<int, float> entry in distances)
        {
            if (entry.Key != fDistances[0]) continue;
            _goToNode = entry.Key;
            print(_goToNode);
            break;
        }

        _canMove = true;
    }
}
