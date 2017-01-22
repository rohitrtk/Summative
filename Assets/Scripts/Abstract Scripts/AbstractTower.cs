using UnityEngine;

/// <summary>
/// All towers will extend this class
/// </summary>
public abstract class AbstractTower : MonoBehaviour {

    [SerializeField] protected LayerMask _playerMask;       // Player mask to collide with
    [SerializeField] protected LayerMask _enemMask;         // Enemy mask to collide with
    [SerializeField] protected Transform _transform;        // Towers transform
    [SerializeField] protected float _cooldownTime;         // How long this tower has to wait before it can fire again 

    [SerializeField] protected Rigidbody _bulletPrefab;     // Bullet reference to shoot
    private float _currentCooldownTime;                     // What is the current cooldown time @
    private bool _onCooldown;                               // Is this tower on cooldown?
    private float radius;                                   // Firing range radius

    /// <summary>
    /// Called by Unity on onject creation
    /// </summary>
    public virtual void Start ()
    {
        radius = GetComponentInChildren<SphereCollider>().radius;
        _onCooldown = false;
        _currentCooldownTime = _cooldownTime;
	}
	
    /// <summary>
    /// Called by Unity
    /// </summary>
	public virtual void Update ()
    {
        // If tower can shoot
        if(!_onCooldown)
        {
            // Find all the enemies on the map (Inefficient I know :/)
            AbstractEnemy[] enemies = FindObjectsOfType(typeof(AbstractEnemy))
                as AbstractEnemy[];
            
            // For each enemy on the map, if they're range...
            foreach(AbstractEnemy p in enemies)
            {
                Vector3 rotationToTarget = p.transform.position - transform.position;
                float distanceToTarget = Vector3.Distance(transform.position, p.transform.position);

                // If there is an enemy with in range, shoot at it and then leave the loop
                if (distanceToTarget <= radius)
                {
                    Vector3 direction = Vector3.RotateTowards(transform.position,
                        rotationToTarget, 100f, 0.0f);
                    
                    transform.GetChild(3).rotation = Quaternion.LookRotation(direction);

                    LaunchProjectile();

                    _onCooldown = true;
                    break; 
                }
            }
        }

        // Cooldown timer 
        _currentCooldownTime -= Time.deltaTime;
        if(_currentCooldownTime <= 0f)
        {
            _currentCooldownTime = _cooldownTime;
            _onCooldown = false;
        }
	}

    /*
    public virtual void OnTriggerEnter(Collider other)
    {
        
        SphereCollider sc = GetComponent<SphereCollider>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, sc.radius, _playerMask);

        foreach (Collider c in colliders)
        {
            Rigidbody targetsRigidbody = c.GetComponent<Rigidbody>();
            if (!targetsRigidbody) continue;

            if(!_onCooldown) LaunchProjectile();
        }
    }
    */

    /// <summary>
    /// Launches the protectile
    /// Actual launching will be done from within child class so
    /// vairability can be add within tower types
    /// </summary>
    public virtual void LaunchProjectile()
    {
        _onCooldown = true;
    }
}