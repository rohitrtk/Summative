using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the MP given for the MP cubes
/// </summary>
public class MPScript : MonoBehaviour {

    private int _amountOfMp;                        // How much mp this cube will yield
        
    /// <summary>
    /// Called by Unity on object creation
    /// </summary>
	void Start ()
    {
        _amountOfMp = (int) Random.Range(10f, 50f);
	}

    /// <summary>
    /// Called by Unity on collision
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            // Collision w/ player
        }
    }

    /// <summary>
    /// Sets the amount of MP for this cube
    /// </summary>
    /// <param name="amountOfMp"></param>
    public void SetAmountOfMp(int amountOfMp)
    {
        _amountOfMp = amountOfMp;
    }

    /// <summary>
    /// Gets the amount of MP for this cube
    /// </summary>
    /// <returns></returns>
    public int GetAmountOfMp()
    {
        return _amountOfMp;
    }
}
