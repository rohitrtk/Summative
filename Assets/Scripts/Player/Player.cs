using UnityEngine;

/// <summary>
/// Extends from AbstractPlayer, acts as the actual player object to be instantiated
/// </summary>
public class Player : AbstractPlayer
{
    /// <summary>
    /// Called by Unity
    /// </summary>
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Called by Unity for physics calculations
    /// </summary>
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}