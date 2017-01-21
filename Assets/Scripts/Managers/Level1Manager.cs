using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The actualy object of AbstractGameManager to be instantiated
/// </summary>
public class Level1Manager : AbstractGameManager {

    /// <summary>
    /// Called by Unity
    /// </summary>
	public override void Start ()
    {
        base.Start();
	}
	
    // Called by Unity
	public override void Update ()
    {
        base.Update();
	}

    /// <summary>
    /// Sets the phase of the current game manager
    /// </summary>
    /// <param name="phase"></param>
    public void SetPhase(bool phase)
    {
        _combatPhase = phase;
    }

    /// <summary>
    /// Gets the phase of the current game manager
    /// </summary>
    /// <returns></returns>
    public bool GetPhase()
    {
        return _combatPhase;
    }
}