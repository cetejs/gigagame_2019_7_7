using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private GameObject player;

    private GameObject Player
    {
        get
        {
            if (player == null)
            {
                player = GameObject.Find(CustomTag.PLAYER);

            }
            return player;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CustomPlayerPrefs.Instance.Health < CustomVariable.HEALTH_MAX) {
            Player.GetComponent<PlayerHealth>().TakeHeart(1);
            Destroy(gameObject);
        }

    }
}
