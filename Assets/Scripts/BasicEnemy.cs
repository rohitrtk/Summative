using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The basic enemy class
/// </summary>
public class BasicEnemy : AbstractEnemy {

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
    /// Called by Unity on collision
    /// </summary>
    /// <param name="other"></param>
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    /// <summary>
    /// Sets this enemies node system, handled by parent classes method
    /// </summary>
    /// <param name="nodes"></param>
    public override void SetNodeSystem(List<Transform> nodes)
    {
        base.SetNodeSystem(nodes);
    }
}