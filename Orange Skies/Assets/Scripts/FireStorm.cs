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
}
