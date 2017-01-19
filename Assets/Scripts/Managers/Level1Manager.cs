using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : AbstractGameManager {

    GameObject g;

	public override void Start ()
    {
        g = GameObject.Find("Player");

        base.Start();

        // Work on sending build bool to HUD
	}
	
	public override void Update ()
    {
		
	}
}
