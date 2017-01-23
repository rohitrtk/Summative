using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// This class handles the setting, getting and displaying of the players MP
/// </summary>
public class PlayerMP : MonoBehaviour
{
    [SerializeField] private Text t;            // Text object to write to
    [SerializeField] private float _baseMp;     // Players base mp value
    [SerializeField] private Slider _slider;    // Slider for the health UI
    [SerializeField] private Image _image;      // Image for the circle thingy

    private float _mp;                          // Players MP
    private const string _MP = "MP: ";          // Const string for UI
    private Color _fullManaColour;              // Colour for the players full mana

    /// <summary>
    /// Called by Unity for object init
    /// </summary>
    public void Start()
    {
        _fullManaColour = Color.blue;
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
        if (_mp < 0f) _mp = 0f; // Safety net for -mp

        UpdateGUI();
    }

    /// <summary>
    /// Called when the player gains MP
    /// </summary>
    /// <param name="gainOfMp"></param>
    public void GainMP(float gainOfMp)
    {
        _mp += gainOfMp;
        if (_mp > 100f) _mp = 100f; // Safety net for mp

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
        //t.text = _MP + _mp.ToString();
        _slider.value = _mp;
        _image.color = _fullManaColour;
    }
}