using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    [SerializeField] private int getHeartCount = 1;
    [SerializeField] private TextMesh t;
    private SpriteRenderer _sr;
    private float _timer = CustomVariable.COOLING_TIME_GETHEART;

    private bool _isCanGetHeart = true;

    private SpriteRenderer SR {
        get {
            if (_sr == null) {
                _sr = GetComponent<SpriteRenderer>();
            }
            return _sr; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        var playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth != null && _isCanGetHeart) {
            if (CustomPlayerPrefs.Instance.Health < CustomVariable.HEALTH_MAX) {
                playerHealth.TakeHeart(getHeartCount);
            }
            _isCanGetHeart = false;
            SR.material.color = Color.gray;
        }
        

    }

    private void Update() {
        if (_isCanGetHeart) return;
        _timer -= Time.deltaTime;
        t.text = Mathf.FloorToInt(_timer).ToString();
        if (!t.gameObject.activeInHierarchy) {
            t.gameObject.SetActive(true);
            SR.material.color = Color.white;
        } 
        if (_timer <= 0) {
            t.gameObject.SetActive(false);
            _isCanGetHeart = true;
            _timer = CustomVariable.COOLING_TIME_GETHEART;
        }
    }
}
