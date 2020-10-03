using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;

    //done in lateupdate so that we can get the player's position correctly before moving camera
    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, 0, -10);
        }
    }
}
