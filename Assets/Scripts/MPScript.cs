using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPScript : MonoBehaviour {

    private int _amountOfMp;

	void Start ()
    {
        _amountOfMp = (int) Random.Range(10f, 50f);
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {

        }
    }

    public void SetAmountOfMp(int amountOfMp)
    {
        _amountOfMp = amountOfMp;
    }

    public int GetAmountOfMp()
    {
        return _amountOfMp;
    }
}
