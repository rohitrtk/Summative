using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractBullet : MonoBehaviour {

    [SerializeField] protected LayerMask _playerMask;     // Player mask to collide with
    [SerializeField] protected LayerMask _enemMask;       // Enemy mask to collide with
    [SerializeField] protected float _damage;       // Damage bullet will do
    [SerializeField] protected float _destroyTime;  // If the bullet doesn't hit anything, despawn after this time
    [SerializeField] protected float _radius = 0.5f;// Radius to collide with

    public virtual void Start ()
    {
        Destroy(gameObject, _destroyTime);
	}

    public virtual void Update ()
    {
	}

    public virtual void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _playerMask);

        // Check for player
        foreach(Collider c in colliders)
        {
            Rigidbody targetsRigidbody = c.GetComponent<Rigidbody>();
            if (!targetsRigidbody) continue;

            PlayerHealth ph = targetsRigidbody.GetComponent<PlayerHealth>();
            ph.TakeDamage(GetDamageDone());
            Destroy(gameObject);
        }

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

    public float GetDamageDone()
    {
        return _damage;
    }
}
