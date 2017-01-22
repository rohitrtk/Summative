using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : AbstractTower {

	public override void Start ()
    {
        base.Start();
	}
	
	public override void Update ()
    {
        base.Update();
	}

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void LaunchProjectile()
    {
        base.LaunchProjectile();

        Rigidbody _bulletInstance = Instantiate(_bulletPrefab, _transform.position, _transform.rotation)
            as Rigidbody;

        _bulletInstance.velocity = _transform.forward * 15f;
    }
}
