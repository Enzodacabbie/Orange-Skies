using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStorm : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        //move to the right at a constant speed
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().Die();
        }
    }

    public void speedUp()
    {
        speed *= 4;
    }
}
