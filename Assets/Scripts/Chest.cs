using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    private int _amountOfMp;

	void Start ()
    {
        _amountOfMp = (int) Random.Range(10f, 50f);
    }
    
    public int GetAmountOfMp()
    {
        return _amountOfMp;
    }
}