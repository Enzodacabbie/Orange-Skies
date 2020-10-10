using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    // Start is called before the first frame update
    public int Score, highScore;
    Text Display;
    void Start()
    {
        Score = 0;
    } 
       

    // Update is called once per frame
    void Update()
    {
        /*Score++; //Will be expanded and complexified later*/
        
        Display.text = Score.ToString();
        if (highScore < Score) highScore = Score;
    }
}
