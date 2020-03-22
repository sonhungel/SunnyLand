using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : MonoBehaviour
{
    public PlayerMovement playerMovement;
   

    public GameObject deathEffect;
   
 
    // Update is called once per frame
    void Awake()
    {
    }
    private void DeadthEffect()
    {

        if (deathEffect != null)
        {
         
            deathEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
           
            Destroy(deathEffect, .5f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerMovement.falling == true)
            {
                DeadthEffect();
                Destroy(gameObject);

            }
        }
    }
}
