// This script just contains functions that get called from clicking on buttons in playmode.
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

    public GameObject mainMenuObject; // The Game Object that holds the Main menu UI elements. (we need a reference to this to switch it on and off).
    public GameObject helpMenuObject; // The Game Object that holds the Help Menu UI elements. (we need a reference to this to switch it on and off).

    // This method runs when the object is loaded or instantiated.
    private void Start()
    {
        #region Development Debug
        // During development, it is useful to have some code to ensure that
        // required properties have been properly set in the ispector.
        // This will give the developer better information about anything they have to fix.
        if (!mainMenuObject)
            Debug.LogError("You need to set the main menu object on " + gameObject.name);
        if (!helpMenuObject)
            Debug.LogError("You need to set the help menu object on " + gameObject.name);
        #endregion
        
        mainMenuObject.SetActive(true);
        helpMenuObject.SetActive(false);
    }

    // This method toggles the UI objects on and off.
    public void ToggleMenuPanels()
    {
        mainMenuObject.SetActive(!mainMenuObject.activeSelf);
        helpMenuObject.SetActive(!helpMenuObject.activeSelf);
    }
    // This method will define what happens when the user presses the start button.
    // It is intneded to be run from a UI component.
    public void PressStartButton()
    {
        Debug.Log("Start button pressed.");
        SceneManager.LoadScene(1);
    }

    // This method will define what happens when the user presses the quit button.
    // It is intneded to be run from a UI component.
    public void PressQuitButton()
    {
        Debug.Log("Quit button pressed.");
    #if (UNITY_EDITOR)
    UnityEditor.EditorApplication.isPlaying = false;
    #else
    Application.Quit();
    #endif
    }
}
