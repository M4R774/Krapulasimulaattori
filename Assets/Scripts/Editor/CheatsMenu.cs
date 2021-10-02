using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatsMenu : MonoBehaviour
{
    [MenuItem("Cheats/Reset Scene", false, 100)]
    static void ResetScene()
    {
        Debug.Log("Reset Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [MenuItem("Cheats/Reset Scene", true, 100)]
    static bool ValidateResetScene()
    {
        return Application.isPlaying;
    }

    [MenuItem("Cheats/Log Selected Transform Name", false, 100)]
    static void LogSelectedTransformName()
    {
        Debug.Log("Selected Transform is on " + Selection.activeTransform.gameObject.name + ".");
    }

    [MenuItem("Cheats/Log Selected Transform Name", true, 100)]
    static bool ValidateLogSelectedTransformName()
    {
        // Return false if no transform is selected.
        return Selection.activeTransform != null;
    }

    [MenuItem("Cheats/Do Something with a Shortcut Key %g", false, 110)]
    static void DoSomethingWithAShortcutKey()
    {
        Debug.Log("Doing something with a Shortcut Key...");
    }

    //[MenuItem("CONTEXT/Rigidbody/Double Mass")]
    //static void DoubleMass(MenuCommand command)
    //{
    //    Rigidbody body = (Rigidbody)command.context;
    //    body.mass = body.mass * 2;
    //    Debug.Log("Doubled Rigidbody's Mass to " + body.mass + " from Context Menu.");
    //}

    //[MenuItem("GameObject/MyCategory/Custom Game Object", false, 10)]
    //static void CreateCustomGameObject(MenuCommand menuCommand)
    //{
    //    // Create a custom game object
    //    GameObject go = new GameObject("Custom Game Object");
    //    // Ensure it gets reparented if this was a context click (otherwise does nothing)
    //    GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
    //    // Register the creation in the undo system
    //    Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
    //    Selection.activeObject = go;
    //}
}