using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Common.Utilities
{
    public class CleanEmptyPlayerPrefsEditorUtility : EditorWindow
    {
        [MenuItem("Tools/Clean Player Prefs")]
        private static void Cleanup()
        {
            PlayerPrefs.DeleteAll();

            Debug.Log("Remove All Player Prefs");
        }
    }
}