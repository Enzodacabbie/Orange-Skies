using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceChecker : MonoBehaviour
{
    public Transform startingPoint;
    public Transform player;
    private TextMeshProUGUI display;
    private int distance;

    private void Awake()
    {
        display = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (player != null)
        {
            distance = (int)(player.position.x - startingPoint.position.x);
            display.text = distance + " M.";
        }
    }
}
