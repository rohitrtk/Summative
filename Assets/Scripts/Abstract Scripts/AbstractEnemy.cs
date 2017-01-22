using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// All enemies will extend this class
/// </summary>
public abstract class AbstractEnemy : MonoBehaviour {

    [SerializeField] private Transform _target;     // Target destination
    private NavMeshAgent _nma;                      // Navmeshagent component

    private List<Transform> _nodes;                 // List of nodes                 
    private int _nodeCount;                         // How many nodes has this object pased
    private Transform _targetNode;

    private List<Transform> _closedNodes;           // List of nodes that this object has collided with
    private bool _canMove;                          // Can this object move?

    /// <summary>
    /// Called by Unity on object creation
    /// </summary>
	public virtual void Start ()
    {
        _nma = GetComponent<NavMeshAgent>();

        _canMove = false;

        _nodeCount = 0;
        _closedNodes = new List<Transform>();
        _targetNode = _nodes[0];
	}

    /// <summary>
    /// Sets this objects node list to a list of Transforms from outside of the class
    /// </summary>
    /// <param name="nodes"></param>
    public virtual void SetNodeSystem(List<Transform> nodes)
    {
        _nodes = nodes;
    }

    /// <summary>
    /// Called by Unity every frame
    /// </summary>
    public virtual void Update ()
    {
        if (_target.position == null) return;
        Path();
	}

    /// <summary>
    /// Called by Unity when an object collides with this object
    /// </summary>
    /// <param name="other"></param>
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

    /// <summary>
    /// Sets the enemies target transform
    /// </summary>
    /// <param name="t"></param>
    public void SetTargetTransform(Transform t)
    {
        _target = t;
    }

    /// <summary>
    /// Used to navigate the node system
    /// </summary>
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

    /// <summary>
    /// Finds the closest node
    /// </summary>
    private void FindNode()
    {
        var distances = new Dictionary<Transform, float>();

        // Foreach node in nodes, add it to the dictionary with it's transform and distance from player
        foreach (Transform t in _nodes)
        {
            // Don't add t to the dictionary if it's in the closed node list
            if (_closedNodes.Contains(t)) continue;
            distances.Add(t, Vector3.Distance(t.position, transform.position));               
        }

        // LINQ expression
        _targetNode = distances.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        //print("Moving to " + _targetNode);
        _canMove = true;
    }
}
