using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the crystals HP and other crystal related stuff
/// </summary>
public class CrystalManager : MonoBehaviour {

    [SerializeField] private float _hp;         // Crystals current health
    [SerializeField] private Slider _slider;    // Slider for the health UI
    [SerializeField] private Image _image;      // Image for the circle thingy

    private const float _baseHealth = 100f;     // Crystal base health
    private Color _zeroHealthColour;            // The zero health colour
    private Color _fullHealthColour;            // The full health colour
    private bool _dead;                         // Is the crystal dead?

    /// <summary>
    /// Called by Unity on object creation
    /// </summary>
	private void Awake ()
    {
        if (_hp > _baseHealth) _hp = _baseHealth;

        _dead = false;

        _zeroHealthColour = Color.red;
        _fullHealthColour = Color.green;

        SetHealthGUI();
    }

    /// <summary>
    /// Called when the crystal loses health
    /// </summary>
    /// <param name="lose"></param>
    public void LoseHP(float lose)
    {
        _hp -= lose;
        
        SetHealthGUI();
        
        if (!_dead && _hp < 1f) Die();
    }

    /// <summary>
    /// Sets the health UI on the crystal
    /// </summary>
    private void SetHealthGUI()
    {
        _slider.value = _hp;
        _image.color = Color.Lerp(_zeroHealthColour, _fullHealthColour, _hp/100f);
    }

    /// <summary>
    /// Called when the crystal dies
    /// </summary>
    private void Die()
    {
        _dead = true;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the HP of the crystal
    /// </summary>
    /// <param name="hp"></param>
    public void SetHP(float hp)
    {
        _hp = hp;
    }

    /// <summary>
    /// Gets the HP of the crystal
    /// </summary>
    /// <returns></returns>
    public float GetHP()
    {
        return _hp;
    }
}