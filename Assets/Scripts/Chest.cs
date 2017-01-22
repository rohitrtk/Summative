using UnityEngine;

/// <summary>
/// Used to return a currency to the player
/// </summary>
public class Chest : MonoBehaviour {

    // Value for how much mp this chest will have
    private int _amountOfMp;

    /// <summary>
    /// Called by Unity on object creation
    /// </summary>
	void Start ()
    {
        _amountOfMp = (int) Random.Range(10f, 50f);
    }
    
    /// <summary>
    /// Get this chests mp amount
    /// </summary>
    /// <returns></returns>
    public int GetAmountOfMp()
    {
        return _amountOfMp;
    }
}