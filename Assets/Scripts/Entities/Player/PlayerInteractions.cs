using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public Player player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Eatable")
        {
            Eatable eatable = collision.gameObject.GetComponent<Eatable>();
            if (eatable.CanBeEatenBy(player))
            {
                player.AddMass(eatable.Mass);
                eatable.Eaten();
            }
        }
    }
}
