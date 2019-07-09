using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;

public class LevelController : Singleton<LevelController> {

    [SerializeField] private List<GameObject> levels;
    [SerializeField] private DOTweenAnimation cutsceneDoAnim;
    [SerializeField] private GameObject[] heartUIs;
    [SerializeField] private GameObject btn;

    private Transform _player;

    public Transform Player {
        get {
            if (_player == null)
                _player = transform.Find(CustomTag.PLAYER);
            return _player;
        }
    }

    public bool IsChangeLeveling { get; private set; }

    private void Update() {
      
        if (InputBase.Instance.IsQuitDown()) {
            ReturnMenu();
        }
    }

    private IEnumerator ChangeLevel_IE(int level,bool isReturnMenu = false) {
        IsChangeLeveling = true;
        cutsceneDoAnim.DOPlayForward();
        yield return new WaitForSeconds(1f);
        SetCurrentLevel(level);
        if (isReturnMenu) {
            foreach (var go in heartUIs) {
                go.SetActive(false);
            }
        }
        btn.SetActive(isReturnMenu);
        Player.gameObject.SetActive(!isReturnMenu);
        cutsceneDoAnim.DOPlayBackwards();
        yield return new WaitForSeconds(1f);
        IsChangeLeveling = false;
    }
    private void SetCurrentLevel(int level) {
        foreach (var levelGo in levels) { levelGo.SetActive(false); }
        levels[level].SetActive(true);
        Player.position = GetPlayerRebirthPos_Die();
        CustomPlayerPrefs.Instance.Health = CustomVariable.HEALTH_MIN;
        Player.GetComponent<PlayerBehavior>().RefreshHeart();
    }

    public void ChangeLevel() {
        Player.gameObject.SetActive(false);
        StartCoroutine(ChangeLevel_IE(CustomPlayerPrefs.Instance.Level));
    }

    public Vector2 GetPlayerRebirthPos_Die() {
        Transform t = levels[CustomPlayerPrefs.Instance.Level].transform.Find("PlayerRebirthPos/Die");
        if (t == null) return Player.position;
        return t.position;
    }

    public Vector2 GetPlayerRebirthPos_Fall() {
        Transform t = levels[CustomPlayerPrefs.Instance.Level].transform.Find("PlayerRebirthPos/Fall");
        if (t == null) return new Vector2(-2.5f, 1.5f);
        Vector2 pos = t.position;
        pos.y = 1.5f;
        return pos;
    }

    public void RefreshHeartUI() {
        foreach (var go in heartUIs) {
            go.SetActive(false);
        }
        for (var i = 0; i < CustomPlayerPrefs.Instance.Health; i++) {
            heartUIs[i].SetActive(true);
        }
    }

    public void ReturnMenu() {
        StartCoroutine(ChangeLevel_IE(0, true));
        Player.GetComponent<PlayerBehavior>().StopInvincible();
    }

}
