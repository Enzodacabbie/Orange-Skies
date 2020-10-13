using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWhenLoaded : MonoBehaviour
{
    protected bool moving;
    protected Rigidbody2D rb;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if a loader object is detected, then behavior can start
        //(the only "loader" in this case should be a big trigger box connected to the camera)
        if (collision.gameObject.CompareTag("Loader")) moving = true;
    }
}
