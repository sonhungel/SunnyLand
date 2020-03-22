using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScript2 : MonoBehaviour
{

    [SerializeField] private float left;
    [SerializeField] private float right;
    [SerializeField] private float speedMove = 1f;


    public PlayerMovement playerMovement;

    public GameObject deathEffect;


    private Rigidbody2D body;



    private bool facingLeft = true;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();


    }


    private void Run()
    {

        if (facingLeft)
        {
            if (transform.position.x > left)
            {

                //kiểm tra để đảo chiều nhân vật
                if (transform.localScale.x != 1.5f)
                {
                    transform.localScale = new Vector2(1.5f, 1.5f);
                }
                body.velocity = new Vector2(-speedMove, 0f);

            }
            else
                facingLeft = false;
        }

        else
        {
            if (transform.position.x < right)
            {
                //kiểm tra để đảo chiều nhân vật
                if (transform.localScale.x != -1.5f)
                {
                    transform.localScale = new Vector2(-1.5f, 1.5f);
                }

                body.velocity = new Vector2(speedMove, 0f);
            }
            else
                facingLeft = true;
        }
    }

    void FixedUpdate()
    {

        Run();
        //Deadth();

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
