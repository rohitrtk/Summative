using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the crystals HP and other crystal related stuff
/// </summary>
public class CrystalManager : MonoBehaviour {

    private const float _baseHealth = 100f;
    [SerializeField] private float _hp;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _image;
    private Color _zeroHealthColour;
    private Color _fullHealthColour;

    private bool _dead;

	void Start ()
    {
        if (_hp > _baseHealth) _hp = _baseHealth;

        _dead = false;

        _zeroHealthColour = Color.red;
        _fullHealthColour = Color.green;

        SetHealthGUI();
    }
	
	void Update ()
    {
        
	}

    public void LoseHP(float lose)
    {
        _hp -= lose;

        SetHealthGUI();

        if (!_dead && _hp < 1f) Die();
    }

    private void SetHealthGUI()
    {
        _slider.value = _hp;
        _image.color = Color.Lerp(_zeroHealthColour, _fullHealthColour, _hp/100f);
    }

    private void Die()
    {
        _dead = true;
        gameObject.SetActive(false);
    }

    public void SetHP(float hp)
    {
        _hp = hp;
    }

    public float GetHP()
    {
        return _hp;
    }
}
