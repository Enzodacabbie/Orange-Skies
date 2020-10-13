using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//See MoveWhenLoaded.cs for more information
public class FoxMovement : MoveWhenLoaded
{
    private void FixedUpdate()
    {
        if (moving)
        {
            //move forward at a constant speed
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }
}
