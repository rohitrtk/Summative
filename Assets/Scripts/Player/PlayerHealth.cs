using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// This class handles the setting, getting and displaying of the players HP
/// </summary>
public class PlayerHealth : MonoBehaviour {

    [SerializeField] private Text t;            // Text object to write to
    [SerializeField] public float _baseHealth;  // The base health of the player
    [SerializeField] private Slider _slider;    // Slider for the health UI
    [SerializeField] private Image _image;      // Image for the circle thingy

    private Color _fullHealthColour;            // The full health colour
    private float _health;                      // Players health
    private const string _HEALTH = "Health: ";  // Const string for UI
    private bool _isDead;                       // Is this player dead?

    /// <summary>
    /// Called by Unity for object init
    /// </summary>
    public void Start()
    {
        _fullHealthColour = Color.red;

        SetHealth(_baseHealth);
        TakeDamage(0f);
    }

    /// <summary>
    /// Called when the player takes damage
    /// </summary>
    /// <param name="damageTaken"></param>
    public void TakeDamage(float damageTaken)
    {
        _health -= damageTaken;
        UpdateGUI();
            
        if (_health < 1f) Die();
    }

    /// <summary>
    /// Called to set the players health
    /// </summary>
    /// <param name="health"></param>
    public void SetHealth(float health)
    {
        _health = health;
        UpdateGUI();
    }

    /// <summary>
    /// Gets the players current health
    /// </summary>
    /// <returns></returns>
    public float GetHealth()
    {
        return _health;
    }
    
    /// <summary>
    /// Updates the health UI on screen
    /// </summary>
    public void UpdateGUI()
    {
        //t.text = _HEALTH + _health.ToString();
        _slider.value = _health;
        _image.color = _fullHealthColour;
    }

    /// <summary>
    /// Called when the player dies
    /// </summary>
    public void Die()
    {
        _isDead = true;
    }
}
