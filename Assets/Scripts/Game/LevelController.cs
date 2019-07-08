using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;

public class LevelController : Singleton<LevelController> {

    [SerializeField] private List<GameObject> levels;
    [SerializeField] private DOTweenAnimation cutsceneDoAnim;

    public bool IsChangeLeveling { get;private set; }

    private Transform _player;

    [SerializeField] private Transform Player;
    //private Transform Player {
    //    get {
    //        if (_player == null)
    //            _player = GameObject.FindWithTag(CustomTag.PLAYER).transform;
    //        if (_player == null)
    //            _player = GameObject.Find(CustomTag.PLAYER).transform;
    //        return _player;
    //    }
    //}

    private IEnumerator ChangeLevel_IE() {
        IsChangeLeveling = true;
        cutsceneDoAnim.DOPlayForward();
        yield return new WaitForSeconds(1f);
        SetCurrentLevel();
        Player.gameObject.SetActive(true);
        cutsceneDoAnim.DOPlayBackwards();
        yield return new WaitForSeconds(1f);
        IsChangeLeveling = false;
        if (CustomPlayerPrefs.Instance.Level == 2) {
            yield return new WaitForSeconds(10f);
            levels[CustomPlayerPrefs.Instance.Level].GetComponent<PlayableDirector>().enabled = false;
        }
    }
    private void SetCurrentLevel() {
        foreach (var levelGo in levels) { levelGo.SetActive(false); }
        levels[CustomPlayerPrefs.Instance.Level].SetActive(true);
        Player.position = GetPlayerRebirthPos_Die();
    }

    public void ChangeLevel() {
        Player.gameObject.SetActive(false);
        StartCoroutine(ChangeLevel_IE());
    }

    public Vector2 GetPlayerRebirthPos_Die() {
        Transform t = levels[CustomPlayerPrefs.Instance.Level].transform.Find("PlayerRebirthPos/Die");
        if (t == null) return Player.position;
        return t.position;
    }

    public Vector2 GetPlayerRebirthPos_Fall() {
        Transform t = levels[CustomPlayerPrefs.Instance.Level].transform.Find("PlayerRebirthPos/Fall");
        if (t == null) return new Vector2(-2.5f, 2f);
        Vector2 pos = t.position;
        pos.y = 1.5f;
        return pos;
    }


}
