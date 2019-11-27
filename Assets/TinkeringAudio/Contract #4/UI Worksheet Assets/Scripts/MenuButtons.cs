//**CONTEXT** This script defines the methods used for the Tinkering Audio assignment.
// The context for this is based on contract #4, and is a User Interface for an imaginary game.
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.UI; // If we are accessing UI components (eg Text, Button, etc.), this library must be used.

public class MenuButtons : MonoBehaviour {

    public GameObject mainMenuObject; // The Game Object that holds the Main menu UI elements. (we need a reference to this to switch it on and off).
    public GameObject helpMenuObject; // The Game Object that holds the Help Menu UI elements. (we need a reference to this to switch it on and off).

    // This method runs when the object is loaded or instantiated.
    // Use this to set the initial state of things and setup  references to objects the script needs.
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

        // It is sometimes useful to show and hide UI elements at the start with script, 
        // rather than rely on them being setup correctly in the scene.
        mainMenuObject.SetActive(true);
        helpMenuObject.SetActive(false);
    }

    // This method toggles the UI objects on and off.
    // It is intneded to be run from a UI component.
    public void ToggleMenuPanels()
    {
        //SetActive() enables or disables a game object.  activeSelf tells us if a game object is active or not. 
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

    // This method will define what happens when the user clicks the help button.
    // It is intneded to be run from a UI component.
    public void PressHelpButton()
    {
        Debug.Log("Help button pressed.");
    }

    // This method will define what happens when the user presses the quit button.
    // It is intneded to be run from a UI component.
    public void PressQuitButton()
    {
        Debug.Log("Quit button pressed.");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
