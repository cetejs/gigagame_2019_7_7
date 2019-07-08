using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject _textGo;
    private bool _isStartGame;
    private void Update() {
        if (Input.anyKeyDown && !_isStartGame) {
            LevelController.Instance.ChangeLevel();
            _textGo.SetActive(false);
            _isStartGame = true;
        }
    }
}
