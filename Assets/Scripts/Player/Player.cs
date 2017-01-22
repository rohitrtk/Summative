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

    /// <summary>
    /// Called by Unity for physics calculations
    /// </summary>
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Called by Unity for render
    /// </summary>
    public override void LateUpdate()
    {
        base.LateUpdate();
    }
}