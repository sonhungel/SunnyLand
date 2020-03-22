using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{

    [SerializeField] private float left;
    [SerializeField] private float right;
    [SerializeField] private float speedMove = 1f;

   

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

                body.velocity = new Vector2(-speedMove, 0f);

            }
            else
                facingLeft = false;
        }

        else
        {
            if (transform.position.x < right)
            {

                body.velocity = new Vector2(speedMove, 0f);
            }
            else
                facingLeft = true;
        }
    }

    void FixedUpdate()
    {

        Run();
       

    }
    
}
