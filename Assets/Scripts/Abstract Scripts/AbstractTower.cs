using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// All towers will extend this class
/// </summary>
public abstract class AbstractTower : MonoBehaviour {

    [SerializeField] protected LayerMask _playerMask;       // Player mask to collide with
    [SerializeField] protected LayerMask _enemMask;         // Enemy mask to collide with
    [SerializeField] protected Transform _transform;        // Towers transform
    [SerializeField] protected float _cooldownTime;         // How long this tower has to wait before it can fire again 
    [SerializeField] protected Rigidbody _bulletPrefab;     // Bullet reference to shoot

    protected float _cost = 10f;                            // Cost of tower

    [SerializeField] private float _hp;                     // Towers current health
    [SerializeField] private Slider _slider;                // Slider for the health UI
    [SerializeField] private Image _image;                  // Image for the circle thingy

    private Color _zeroHealthColour;                        // The zero health colour
    private Color _fullHealthColour;                        // The full health colour
    private const float _baseHealth = 100f;                 // Crystal base health
    private float _currentCooldownTime;                     // What is the current cooldown time @
    private float radius;                                   // Firing range radius
    private bool _dead;                                     // Is the crystal dead?
    private bool _onCooldown;                               // Is this tower on cooldown?

    /// <summary>
    /// Called by Unity on onject creation
    /// </summary>
    public virtual void Start ()
    {
        radius = GetComponentInChildren<SphereCollider>().radius;
        _onCooldown = false;
        _currentCooldownTime = _cooldownTime;

        _dead = false;
        _zeroHealthColour = Color.red;
        _fullHealthColour = Color.green;
        SetHealthGUI();
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
                    
                    transform.GetChild(1).rotation = Quaternion.LookRotation(direction);

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

    /// <summary>
    /// Sets the health UI for the tower
    /// </summary>
    public void SetHealthGUI()
    {
        _slider.value = _hp;
        _image.color = _image.color = Color.Lerp(_zeroHealthColour, _fullHealthColour, _hp / 100f);
    }

    /// <summary>
    /// Called when the tower dies
    /// </summary>
    public void Die()
    {
        _dead = false;
        Destroy(gameObject);
    }

    /// <summary>
    /// Called when the tower takes damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        _hp -= damage;
        SetHealthGUI();

        if (_hp <= 1f) Die();
    }
     
    /// <summary>
    /// Used to get the cost of the tower
    /// </summary>
    /// <returns></returns>
    public float GetCost()
    {
        return _cost;
    }
}