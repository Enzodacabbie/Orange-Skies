using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenWhenLoaded : MonoBehaviour
{
    public GameEvent addChunkTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if a loader object is detected, then behavior can start
        //(the only "loader" in this case should be a big trigger box connected to the camera)
        if (collision.gameObject.CompareTag("Loader"))
        {
            addChunkTrigger.Raise();
            gameObject.SetActive(false);
        }
    }
}
