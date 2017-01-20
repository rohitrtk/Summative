using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : AbstractBullet {

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
}