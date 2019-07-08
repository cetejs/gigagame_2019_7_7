using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private GameObject player;
    public bool disappear=false;
    private GameObject Player
    {
        get
        {
            if (player == null)
            {
                player = GameObject.FindWithTag(CustomTag.PLAYER);

            }
            return player;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(CustomTag.PLAYER))
        {
            Player.GetComponent<PlayerHealth>().GetDamage(1);

        }
        if (disappear)
        {
            Destroy(gameObject);
        }
    }
}
