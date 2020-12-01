using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWhenLoaded : MonoBehaviour
{
    public GameEvent removeChunkTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if a loader object is detected, then behavior can start
        //(the only "loader" in this case should be a big trigger box connected to the camera)
        if (collision.gameObject.CompareTag("Storm"))
        {
            removeChunkTrigger.Raise();
            gameObject.SetActive(false);
        }
    }
}
