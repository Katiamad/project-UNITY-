using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Timercounter : MonoBehaviour
{
    
    public TextMeshProUGUI text;
    public int timeleft = 240;
    public bool timer = false;

    void start ()
    {
        text.text = timeleft + "s" ;

    }

    void Update () 
    {
        if(timer == false && timeleft > 0) 
        {
            StartCoroutine(timeri());
        }

         if (timeleft <= 0)
        {
            Application.Quit();
        }
    }

    IEnumerator timeri ()
    {
        timer = true;
        yield return new WaitForSeconds(1);
        timeleft -= 1;
        text.text = timeleft + "s";
        timer = false;
    }
       

    
}
