using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float fullHealth;
    float currentHealth;
    public bool playerDied = false;
    public GameObject playerObj;


    PlayerMovement controller; 
    

    public Canvas playerCanvas;
    public Slider playerHealthSlider;
    public AudioSource source;
    public AudioClip clip;
    
    void Awake()
    {
       currentHealth = fullHealth;
       playerHealthSlider.minValue = 0;
       playerHealthSlider.maxValue = fullHealth;
       playerHealthSlider.value = currentHealth; 

       controller = GetComponent<PlayerMovement>();
       
    }

    public void AddDamage(float damage)
    {
        currentHealth -= damage;
        playerHealthSlider.value = currentHealth; 

        if (currentHealth <=0)
        {
            playerDied = true;
            playerCanvas.enabled = false;
            source.PlayOneShot(clip);
            controller.Death();
            controller.enabled = false;
            Application.Quit();
            

        }
        
        
    }
    
}
