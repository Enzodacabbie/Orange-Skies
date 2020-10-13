using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminateHazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if hit by the player, destroy this object (or the parent object)
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
