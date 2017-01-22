using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractEnemy : MonoBehaviour {

    [SerializeField] private Transform _target;     // Target destination
    private NavMeshAgent _nma;                      // Navmeshagent component

    private List<Transform> _nodes;                 // List of nodes                 
    private int _nodeCount;
    private Transform _targetNode;

    private List<Transform> _closedNodes;
    private bool _canMove;

	public virtual void Start ()
    {
        _canMove = false;
        _nodeCount = 0;
        _closedNodes = new List<Transform>();
        _targetNode = _nodes[0];
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

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 13)
        {
            ++_nodeCount;
            _canMove = false;
            _closedNodes.Add(other.transform);
        }
        else if(other.gameObject.layer == 14)
        {
            Destroy(gameObject);
        }
    }

    public void SetTargetTransform(Transform t)
    {
        _target = t;
    }

    private void Path()
    {
        if(_canMove)
        {
            _target = _targetNode;
            _nma.SetDestination(_target.position);

            return;
        }
        FindNode();
    }

    private void FindNode()
    {
        var distances = new Dictionary<Transform, float>();

        foreach (Transform t in _nodes)
        {
            if (_closedNodes.Contains(t)) continue;
            distances.Add(t, Vector3.Distance(t.position, transform.position));               
        }

        //List<float> fDistances = distances.Values.ToList();
        //Array.Sort(fDistances.ToArray());

        //_targetNode = distances.Values.ToList().IndexOf(fDistances[0]);
        // Research LINQ expressions!
        //_targetNode = distances.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        //if (!_targetNode == _nodes[0]) _targetNode = distances.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        _targetNode = distances.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        print("Moving to " + _targetNode);
        _canMove = true;
    }
}
