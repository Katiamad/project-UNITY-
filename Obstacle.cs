using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    public float playerDamageAmount;
    public bool playerInRange = false;
    public DateTime nextDamage;
    public float damageAfterTime;


    public GameObject playerObj;

   

    // Update is called once per frame
    void awake()
    {
        nextDamage = DateTime.Now;
        
    }

    void FixedUpdate()
    {
        if (playerInRange == true) 
        {
            DamagePlayer();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<Health>().playerDied == false)
            {
                playerObj = other.gameObject;
                playerInRange = true;
                Application.Quit();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") 
        {
            playerInRange = false;
        }
    }

    public void DamagePlayer()
    {
        if (nextDamage <= DateTime.Now)
        {

            if (playerObj.GetComponent<Health>().playerDied == false) 
            {
                playerObj.GetComponent<Health>().AddDamage(playerDamageAmount);
                nextDamage = DateTime.Now.AddSeconds(System.Convert.ToDouble(damageAfterTime));
                
            }



            
        }
    }
}
