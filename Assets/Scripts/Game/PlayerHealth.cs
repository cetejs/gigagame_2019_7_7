using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBehavior))]
public class PlayerHealth : MonoBehaviour
{
    private PlayerBehavior _playerBehavior;
    private PlayerBehavior PlayerBehavior {
        get {
            if (_playerBehavior == null) {
                _playerBehavior = GetComponent<PlayerBehavior>();
            }
            return _playerBehavior;
        }
    }

  

    public void GetDamage(int damage) {
        var health = CustomPlayerPrefs.Instance.Health;
        if (health > 0) {
            health -= damage;
            if (health > 0) PlayerBehavior.Invincible(2f);
        }
        if (health <= 0) {
            health = 0;
            PlayerBehavior.Die();
        }
        CustomPlayerPrefs.Instance.Health = health;
        PlayerBehavior.RefreshHeart();
    }

    public void TakeHeart(int heartCount) {
        CustomPlayerPrefs.Instance.Health += heartCount;
        PlayerBehavior.RefreshHeart();
    }

}
