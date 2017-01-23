using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The basic bullet type
/// Used by the Basic tower
/// </summary>
public class BasicBullet : AbstractBullet {

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
    /// Called by Unity on object collision
    /// </summary>
    /// <param name="other"></param>
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}