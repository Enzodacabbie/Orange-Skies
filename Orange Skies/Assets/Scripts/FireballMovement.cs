using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//See MoveWhenLoaded.cs for more information
public class FireballMovement : MoveWhenLoaded
{
    public GameObject flash;
    private GameObject beginFlash;
    public float flashTime;
    public GameObject debrisParticles;
    private Vector2 movement;

    private void Start()
    {
        //cache it for efficiency
        movement = Vector2.up * speed;
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            //moveposition should always be used when directly changing the position of a rigidbody
            //moves the fireball down at a constant speed
            rb.MovePosition(rb.position - movement * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //kills any enemies it touches
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }

        //layer 8 is the ground layer, i got lazy, sorry - V.P
        if (collision.gameObject.layer == 8)
        {
            //spawn debris particles for effect
            Instantiate(debrisParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    //OVERRIDE FROM PARENT
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Loader"))
        {
            StartCoroutine(BeginFall());
        }
    }

    //this is a coroutine (methods which can suspend operations at any time)
    //please message me if you'd like a more understandable version because
    //coroutines are written weird - V.P
    IEnumerator BeginFall()
    {
        //turn on the flash sprite telegraph where the fireball will land
        beginFlash = Instantiate(flash, transform.position, Quaternion.identity);

        //wait a bit before turning off the flash sprite
        yield return new WaitForSeconds(flashTime);
        beginFlash.SetActive(false);
        moving = true;
    }

    private void OnDestroy()
    {
        //for some reason this is buggy
        Destroy(beginFlash);
    }
}
