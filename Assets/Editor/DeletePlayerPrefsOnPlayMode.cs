#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
//BORRAR EN PRODUCCION
//BORRAAAAAR
public static class ResetPlayerPrefsOnPlay
{
    static ResetPlayerPrefsOnPlay()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }
    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            //PlayerPrefs.DeleteKey("UnlockedLevels"); 
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs's Keys has been deleted");
        }
    }
}

#endif
