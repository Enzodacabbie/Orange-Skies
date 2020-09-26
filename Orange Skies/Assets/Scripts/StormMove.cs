using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormMove : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //temporary, absolutely don't do this
            Destroy(collision.gameObject);
        }
    }
}
