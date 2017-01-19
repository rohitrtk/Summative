using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// This class handles the setting, getting and displaying of the players HP
/// </summary>
public class PlayerHealth : MonoBehaviour {

    [SerializeField] private Text t;            // Text object to write to
    private float _health;                      // Players health
    private const string _HEALTH = "Health: ";  // Const string for UI
    private bool _isDead;                       // Is this player dead?

    /// <summary>
    /// Called when the player takes damage
    /// </summary>
    /// <param name="damageTaken"></param>
    public void TakeDamage(float damageTaken)
    {
        _health -= damageTaken;

        if (_health < 1f)
        {
            _isDead = false;
            _health = 0f;
        }
        t.text = _HEALTH + _health.ToString();
    }

    /// <summary>
    /// Called to set the players health
    /// </summary>
    /// <param name="health"></param>
    public void SetHealth(float health)
    {
        _health = health;
    }

    /// <summary>
    /// Gets the players current health
    /// </summary>
    /// <returns></returns>
    public float GetHealth()
    {
        return _health;
    }
}
