using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the players phase UI
/// </summary>
public class PlayerPhase : MonoBehaviour {

    [SerializeField] public Text t;                     // Text reference
    private const string _combatPhaseString = "COMBAT"; // Const string for UI
    private const string _buildPhaseString = "BUILD";   // Const string for UI
    private bool _combatPhase;                          // Current phase ingame

    /// <summary>
    /// Called by Unity for object init
    /// </summary>
    public void Start()
    {
        _combatPhase = false;
        t.text = _buildPhaseString;
    }

    /// <summary>
    /// Sets the current phase for the player based on combat phase of manager
    /// </summary>
    /// <param name="_combatPhase"></param>
    public void SetCurrentPhase(bool _combatPhase)
    {
        this._combatPhase = _combatPhase;
        if (_combatPhase) t.text = _combatPhaseString;
        else t.text = _buildPhaseString;
    }

    /// <summary>
    /// Gets the current phase for the player
    /// </summary>
    /// <returns></returns>
    public bool GetCurrentPhase()
    {
        return _combatPhase;
    }
}
