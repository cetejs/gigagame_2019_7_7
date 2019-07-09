using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject _textGo;
    private bool _isStartGame;
    

    public void OnChangeLevelBtnClick(int level) {
        CustomPlayerPrefs.Instance.Level = level;
        LevelController.Instance.ChangeLevel();
    }

    public void OnKeepGameBtnClick() {
        LevelController.Instance.ChangeLevel();
    }

    public void OnQuitGameBtnClick() {
        Application.Quit();
    }
}
