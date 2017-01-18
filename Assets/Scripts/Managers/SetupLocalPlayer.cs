using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

    public Camera CharacterCamera;
    public AudioListener AudioListener;

	void Start ()
    {
		if(isLocalPlayer)
        {
            GameObject.Find("Main Camera").SetActive(false);

            gameObject.GetComponent<Player>().enabled = true;
            CharacterCamera.enabled = true;
            AudioListener.enabled = true;

            //Renderer[] renderers = GetComponentsInChildren<Renderer>();
            //foreach(Renderer renderer in renderers)
            //{
              //  renderer.enabled = false;
            //}

            GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
        }
	}

    public override void PreStartClient()
    {
        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }
}