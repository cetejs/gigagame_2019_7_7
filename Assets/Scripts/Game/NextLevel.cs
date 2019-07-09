using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
   
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int level = CustomPlayerPrefs.Instance.Level;
        CustomPlayerPrefs.Instance.Level = level + 1;
        LevelController.Instance.ChangeLevel();
    }
}

