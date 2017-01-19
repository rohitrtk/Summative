using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// This class handles the setting, getting and displaying of the players MP
/// </summary>
public class PlayerMP : MonoBehaviour
{
    [SerializeField] private Text t;        // Text object to write to
    [SerializeField] private float _baseMp;
    private float _mp;                      // Players MP
    private const string _MP = "MP: ";      // Const string for UI

    /// <summary>
    /// Called by Unity for object init
    /// </summary>
    public void Start()
    {
        SetMP(_baseMp);
        LoseMP(0f);
    }

    /// <summary>
    /// Called when player loses MP
    /// </summary>
    /// <param name="lossOfMP"></param>
    public void LoseMP(float lossOfMP)
    {
        _mp -= lossOfMP;

        UpdateGUI();
    }

    /// <summary>
    /// Sets the players MP
    /// </summary>
    /// <param name="mp"></param>
    public void SetMP(float mp)
    {
        _mp = mp;
        UpdateGUI();
    }

    /// <summary>
    /// Gets the players current MP
    /// </summary>
    /// <returns></returns>
    public float GetMP()
    {
        return _mp;
    }

    /// <summary>
    /// Called to update the players MP HUD
    /// </summary>
    public void UpdateGUI()
    {
        t.text = _MP + _mp.ToString();
    }
}