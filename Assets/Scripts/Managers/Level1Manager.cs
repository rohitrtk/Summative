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
	
    /// <summary>
    /// Called by Unity every frame
    /// </summary>
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

    /// <summary>
    /// Called by buttons to set the state of the game
    /// </summary>
    /// <param name="s"></param>
    public void SetState(string s)
    {
        if (s.Equals("Play")) _gameState = State.Setup;
        else if (s.Equals("Options")) _gameState = State.Options;
        else if (s.Equals("Exit")) Application.Quit();
        else if (s.Equals("Menu")) _gameState = State.Menu;
    }
}