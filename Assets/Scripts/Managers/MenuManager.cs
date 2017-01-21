using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadOptions()
    {
        Debug.Log("Options");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
