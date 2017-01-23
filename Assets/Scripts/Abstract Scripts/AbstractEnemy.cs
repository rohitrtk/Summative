using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// All enemies will extend this class
/// </summary>
public abstract class AbstractEnemy : MonoBehaviour {

    [HideInInspector] public GameObject CrystalInstance;              // Crystal prefab to target
    [HideInInspector] public Player PlayerInstance;                   // Player to target;
    [HideInInspector] public AbstractTower TowerInstance;             // Tower instance to target
    [SerializeField] private Transform _target;         // Target destination
    [SerializeField] private float _attackCooldown;     // How long to wait between attacks
    private NavMeshAgent _nma;                          // Navmeshagent component

    private List<Transform> _nodes;                     // List of nodes                 
    private int _nodeCount;                             // How many nodes has this object pased
    private Transform _targetNode;

    private List<Transform> _closedNodes;               // List of nodes that this object has collided with
    private bool _canMove;                              // Can this object move?
    private bool _attacking;
    private float _damage;
    private float _currentCooldownTime;

    /// <summary>
    /// Called by Unity on object creation
    /// </summary>
	public virtual void Start ()
    {
        _nma = GetComponent<NavMeshAgent>();
        _attacking = false;
        _canMove = false;
        _damage = 10f;
        _nodeCount = 0;
        _closedNodes = new List<Transform>();
        //_targetNode = _nodes[0];
        _currentCooldownTime = 0;
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

        AbstractTower[] ats = FindObjectsOfType(typeof(AbstractTower))
            as AbstractTower[];
        var distances = new Dictionary<AbstractTower, float>();

        foreach (AbstractTower at in ats)
        {
            distances.Add(at, Vector3.Distance(at.transform.position, transform.position));
        }
        TowerInstance = distances.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;

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

    private bool Attack(Transform t, float distance)
    {
        if (Vector3.Distance(transform.position, t.position) < distance)
        {
            _target = t;
            _nma.SetDestination(_target.position);
            _nma.stoppingDistance = distance;

            if(!_attacking)
            {
                // player
                if (t.gameObject.layer == 8)
                {
                    PlayerHealth ph = PlayerInstance.GetComponent<PlayerHealth>();
                    ph.SetHealth(ph.GetHealth() - _damage);
                }
                // Crystal
                else if (t.gameObject.layer == 11)
                {
                    CrystalManager cm = CrystalInstance.GetComponent<CrystalManager>();
                    cm.LoseHP(_damage);
                }
                // Tower
                else if(t.gameObject.layer == 10)
                {
                    AbstractTower at = TowerInstance.GetComponent<AbstractTower>();
                    at.TakeDamage(_damage);
                }

                _attacking = true;
            }
            else
            {
                _currentCooldownTime -= Time.deltaTime;
                if(_currentCooldownTime <= 0f)
                {
                    _attacking = false;
                    _currentCooldownTime = _attackCooldown;
                }
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Used to navigate the node system
    /// </summary>
    private void Path()
    {
        if (Attack(PlayerInstance.transform, 2f) 
            || Attack(CrystalInstance.transform, 5f)
            || Attack(TowerInstance.transform, 3f)) return;

        _nma.stoppingDistance = 0f;

        if (_canMove)
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
        
        _canMove = true;
    }
}
