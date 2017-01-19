using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// This class handles the setting, getting and displaying of the players MP
/// </summary>
public class PlayerMP : MonoBehaviour
{
    [SerializeField] private Text t;        // Text object to write to
    private float _mp;                      // Players MP
    private const string _MP = "MP: ";      // Const string for UI

    /// <summary>
    /// Called when player loses MP
    /// </summary>
    /// <param name="lossOfMP"></param>
    public void LoseMP(float lossOfMP)
    {
        _mp -= lossOfMP;

        t.text = _MP + _mp.ToString();
    }

    /// <summary>
    /// Sets the players MP
    /// </summary>
    /// <param name="mp"></param>
    public void SetMP(float mp)
    {
        _mp = mp;
    }

    /// <summary>
    /// Gets the players current MP
    /// </summary>
    /// <returns></returns>
    public float GetMP()
    {
        return _mp;
    }
}