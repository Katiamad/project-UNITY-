using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int heart;
    
    
    
    public void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Heart")

        {
            Debug.Log("Heart collected!");
            heart = heart + 1;
           // Col.gameObject.SetActive(false);
            Destroy(Col.gameObject);
        }
    }
    
}
