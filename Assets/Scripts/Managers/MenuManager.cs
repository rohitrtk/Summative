using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used when the game is launched, handles methods for button presses
/// </summary>
public class MenuManager : MonoBehaviour {

    /// <summary>
    /// Load parameter scene
    /// </summary>
    /// <param name="level"></param>
	public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);
    }

    /// <summary>
    /// Loads options...
    /// </summary>
    public void LoadOptions()
    {
        Debug.Log("Options");
    }

    /// <summary>
    /// Closes the application
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}