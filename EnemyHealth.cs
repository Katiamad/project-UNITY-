using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public float fullHealth;
    public float currentHealth;
    public bool enemyDied = false;

    public Canvas enemyCanvas;
    public Slider enemyHealthSlider;
    private EnemyController enemyControll;

    void Awake()
    {
        currentHealth = fullHealth;
        enemyHealthSlider.minValue = 0;
        enemyHealthSlider.maxValue = fullHealth;
        enemyHealthSlider.value = currentHealth;

        enemyControll =  GetComponent<EnemyController>();

    }



    public void AddDamage(float damage)
    {
        currentHealth -= damage;
        enemyHealthSlider.value = currentHealth; 

        if (currentHealth <=0)
        {
            enemyDied = true;
            enemyCanvas.enabled = false;
            MakeDied();
            
        }
    }

    public void MakeDied()
    {
        enemyControll.Death();
        Destroy(gameObject, 3f);
        
    }
}
