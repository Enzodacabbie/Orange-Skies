using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    //optional
    public float targetOffset;

    //LateUpdate occurs after Update is called
    //wait for the object to move, then the camera does the same
    private void LateUpdate()
    {
        //only match the target's xPosition
        if (target != null)
            transform.position = new Vector3(target.transform.position.x + targetOffset, transform.position.y, transform.position.z);
    }
}
