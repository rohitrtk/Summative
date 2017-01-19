using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the changing of the players UI round number
/// </summary>
public class PlayerRoundNumber : MonoBehaviour {

    [SerializeField] private Text t;            // Text reference
    private int _roundNumber;                   // Current round number
    private const string _roundText = "Round: ";// Const string for UI

    /// <summary>
    /// Called by Unity upon object init
    /// </summary>
    public void Start()
    {
        SetRoundNumber(0);
    }

    /// <summary>
    /// Sets the round number
    /// </summary>
    /// <param name="roundNumber"></param>
    public void SetRoundNumber(int roundNumber)
    {
        _roundNumber = roundNumber;
        UpdateGUI();
    }

    /// <summary>
    /// Gets the round number
    /// </summary>
    /// <returns></returns>
    public int GetRoundNumber()
    {
        return _roundNumber;
    }

    /// <summary>
    /// Updates the HUD
    /// </summary>
    private void UpdateGUI()
    {
        t.text = _roundText + _roundNumber.ToString();
    }
}