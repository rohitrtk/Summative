using UnityEngine;

/// <summary>
/// The basic tower class
/// </summary>
public class BasicTower : AbstractTower {

    /// <summary>
    /// Called by Unity on object creation
    /// </summary>
	public override void Start ()
    {
        base.Start();
	}
	
    /// <summary>
    /// Called by Unity every frame
    /// </summary>
	public override void Update ()
    {
        base.Update();
	}

    /// <summary>
    /// Launches the projectile for this tower
    /// </summary>
    public override void LaunchProjectile()
    {
        base.LaunchProjectile();

        // Instantiate the bullet
        Rigidbody _bulletInstance = Instantiate(_bulletPrefab, _transform.position, _transform.rotation)
            as Rigidbody;

        // Add force to the bullet
        _bulletInstance.velocity = _transform.forward * 15f;
    }
}
