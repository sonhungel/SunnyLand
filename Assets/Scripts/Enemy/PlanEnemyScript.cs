using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanEnemyScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    public PlayerMovement playerMovement;

    public GameObject deathEffect;


    private Animator animator;

    private bool isShooting=false;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    void Start()
    {
        StartCoroutine(Attack());
    }

    private void FixedUpdate()
    {
        Shoot();
        isShooting = false;

    }

    IEnumerator Attack()
    {

        yield return new WaitForSeconds(Random.Range(1, 4));
        isShooting = true;
       
        StartCoroutine(Attack());
    }


    private void Shoot()
    {
        if(isShooting==true)
        {
            animator.SetBool("Attack", true);
            Instantiate(bullet, transform.position, Quaternion.identity);
            
        }
        else if(isShooting==false)
        {
            animator.SetBool("Attack", false);
        }
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

