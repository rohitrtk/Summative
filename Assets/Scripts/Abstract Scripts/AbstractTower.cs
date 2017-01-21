using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AbstractTower : MonoBehaviour {

    [SerializeField] protected LayerMask _playerMask;     // Player mask to collide with
    [SerializeField] protected LayerMask _enemMask;       // Enemy mask to collide with
    [SerializeField] protected Transform _transform;
    [SerializeField] protected float _cooldownTime;
    [SerializeField] protected Rigidbody _bulletPrefab;

    private float radius;
    private float _currentCooldownTime;
    private bool _onCooldown;

    public virtual void Start ()
    {
        radius = GetComponentInChildren<SphereCollider>().radius;
        Debug.Log(radius);
        _onCooldown = false;
        _currentCooldownTime = _cooldownTime;
	}
	
	public virtual void Update ()
    {
        //GameObject[] gameObjectArray = FindObjectsOfType(typeof(GameObject)) 
        //    as GameObject[];
        if(!_onCooldown)
        {
            AbstractPlayer[] players = FindObjectsOfType(typeof(AbstractPlayer))
            as AbstractPlayer[];
            
            foreach(AbstractPlayer p in players)
            {
                //Vector3.Distance(transform.position, p.transform.position)
                Vector3 rotationToTarget = p.transform.position - transform.position;
                float distanceToTarget = Vector3.Distance(transform.position, p.transform.position);

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

        _currentCooldownTime -= Time.deltaTime;
        if(_currentCooldownTime <= 0f)
        {
            _currentCooldownTime = _cooldownTime;
            _onCooldown = false;
        }
	}

    public virtual void OnTriggerEnter(Collider other)
    {
        /*
        SphereCollider sc = GetComponent<SphereCollider>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, sc.radius, _playerMask);

        foreach (Collider c in colliders)
        {
            Rigidbody targetsRigidbody = c.GetComponent<Rigidbody>();
            if (!targetsRigidbody) continue;

            if(!_onCooldown) LaunchProjectile();
        }*/
    }

    public virtual void LaunchProjectile()
    {
        _onCooldown = true;
    }
}