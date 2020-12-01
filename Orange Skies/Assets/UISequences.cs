using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISequences : MonoBehaviour
{
    public Animator panelAnim;
    public Animator distanceAnim;
    public Animator restartButtonAnim;
    public Animator quitButtonAnim;

    public void deathSequenceStart()
    {
        StartCoroutine(deathSequence());
    }

    IEnumerator deathSequence()
    {
        //wait 2 seconds
        yield return new WaitForSeconds(2);

        //main panel fade in
        panelAnim.SetTrigger("fade");

        //wait 1 second
        yield return new WaitForSeconds(1);

        //fade in distance counter
        distanceAnim.SetTrigger("fadeIn");

        //wait 1 second
        yield return new WaitForSeconds(1);

        //move distance counter to center
        distanceAnim.SetTrigger("moveToCenter");

        //wait 1 second
        yield return new WaitForSeconds(2);

        restartButtonAnim.gameObject.SetActive(true);
        quitButtonAnim.gameObject.SetActive(true);

        //show button
        restartButtonAnim.SetTrigger("fade");
        quitButtonAnim.SetTrigger("fade");
    }

    public void restartScene()
    {
        print("restarting");
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        print("quitting");
        //go to menu
    }
}
