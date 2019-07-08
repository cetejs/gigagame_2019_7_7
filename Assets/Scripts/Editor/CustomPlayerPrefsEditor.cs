using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomPlayerPrefsEditor : Editor
{
    [MenuItem("CustomTools/ClearPlayerPrefs")]
    static void ClearPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }
}
