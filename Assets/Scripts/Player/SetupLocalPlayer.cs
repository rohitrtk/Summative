using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Handles setting up the player on this computer
/// NOT USED!
/// </summary>
public class SetupLocalPlayer : NetworkBehaviour {

    public Camera CharacterCamera;          // Camera object for this computer
    public AudioListener AudioListener;     // Audio object for this computer

    /// <summary>
    /// Called by Unity for object init
    /// </summary>
	void Start ()
    {
        // If we're not a local player, leave method
        if (!isLocalPlayer) return;

        GameObject.Find("Main Camera").SetActive(false);

        gameObject.GetComponent<Player>().enabled = true;
        CharacterCamera.enabled = true;
        AudioListener.enabled = true;

        GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);
    }
}