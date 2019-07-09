using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    [SerializeField] private int demage = 1;

    private void OnTriggerEnter2D(Collider2D collision) {
        var playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth != null) {
            if (collision.GetComponent<PlayerBehavior>().IsInvincibleing) return;
            if (CustomPlayerPrefs.Instance.Health >= CustomVariable.HEALTH_MIN) {
                playerHealth.GetDamage(demage);
            }
        }

    }

}
