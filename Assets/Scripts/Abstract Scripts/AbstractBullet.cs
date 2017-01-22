using UnityEngine;

/// <summary>
/// All bullets will extend this class
/// </summary>
public abstract class AbstractBullet : MonoBehaviour {

    [SerializeField] protected LayerMask _playerMask;     // Player mask to collide with
    [SerializeField] protected LayerMask _enemMask;       // Enemy mask to collide with
    [SerializeField] protected float _damage;           // Damage bullet will do
    [SerializeField] protected float _destroyTime;      // If the bullet doesn't hit anything, despawn after this time
    [SerializeField] protected float _radius = 0.5f;    // Radius to collide with

    /// <summary>
    /// Called by Unity on object creation
    /// </summary>
    public virtual void Start ()
    {
        Destroy(gameObject, _destroyTime);
	}

    /// <summary>
    /// Called by Unity every frame
    /// </summary>
    public virtual void Update ()
    {
	}

    /// <summary>
    /// Called upon trigger entry on object
    /// </summary>
    /// <param name="other"></param>
    public virtual void OnTriggerEnter(Collider other)
    {
        // Check for player first
        // Get the colliders from the player mask
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _playerMask);

        foreach(Collider c in colliders)
        {
            Rigidbody targetsRigidbody = c.GetComponent<Rigidbody>();
            if (!targetsRigidbody) continue;

            PlayerHealth ph = targetsRigidbody.GetComponent<PlayerHealth>();
            ph.TakeDamage(GetDamageDone());
            Destroy(gameObject);
        }

        // Check for enemy
        colliders = Physics.OverlapSphere(transform.position, _radius, _enemMask);

        // Check for enemies
        foreach (Collider c in colliders)
        {
            Rigidbody targetsRigidbody = c.GetComponent<Rigidbody>();
            if (!targetsRigidbody) continue;

            //PlayerHealth ph = targetsRigidbody.GetComponent<PlayerHealth>();
            //ph.TakeDamage(GetDamageDone());
            //Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called by other object to get the damage done by this bullet
    /// </summary>
    /// <returns></returns>
    public float GetDamageDone()
    {
        return _damage;
    }
}
