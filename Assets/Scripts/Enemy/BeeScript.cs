using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScript : MonoBehaviour
{
    [SerializeField] private float celling;
    [SerializeField] private float ground;
    [SerializeField] private float speedMove = 1f;



    public PlayerMovement playerMovement;

    public GameObject deathEffect;


    private Rigidbody2D body;



    private bool moveDown= true;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();


    }


    private void Run()
    {

        if (moveDown)
        {
            if (gameObject.transform.position.y < celling)
            {

                body.velocity = new Vector2(0f, speedMove);

            }
            else
                moveDown = false;
        }

        else
        {
            if (gameObject.transform.position.y > ground)
            {
                
                body.velocity = new Vector2(0f,-speedMove);
            }
            else
                moveDown = true;
        }
    }

    void FixedUpdate()
    {

        Run();
        Flip();

    }

    private void Flip()
    {
        if(body.transform.position.x<playerMovement.transform.position.x)
        {
            transform.localScale = new Vector2(-3f, 3f);
        }
        else if(body.transform.position.x > playerMovement.transform.position.x)
        {
            transform.localScale = new Vector2(3f, 3f);
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
