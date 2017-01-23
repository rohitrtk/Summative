using UnityEngine;

/// <summary>
/// All bullets will extend this class
/// </summary>
public abstract class AbstractBullet : MonoBehaviour {

    [SerializeField] protected LayerMask _playerMask;    // Player mask to collide with
    [SerializeField] protected LayerMask _enemMask;      // Enemy mask to collide with
    [SerializeField] protected float _damage;           // Damage bullet will do
    [SerializeField] protected float _destroyTime;      // If the bullet doesn't hit anything, despawn after this time
    [SerializeField] protected float _radius = 0.5f;    // Radius to collide with
    protected float _waitTime;                          // Time to wait before bullet collision

    /// <summary>
    /// Called by Unity on object creation
    /// </summary>
    public virtual void Start ()
    {
        _waitTime = _destroyTime / 2;
        Destroy(gameObject, _destroyTime);
	}

    /// <summary>
    /// Called by Unity every frame
    /// </summary>
    public virtual void Update ()
    {
        _waitTime -= Time.deltaTime;
	}

    /// <summary>
    /// Called upon trigger entry on object
    /// </summary>
    /// <param name="other"></param>
    public virtual void OnTriggerEnter(Collider other)
    {
        if(_waitTime <= 0f) Destroy(gameObject);
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
