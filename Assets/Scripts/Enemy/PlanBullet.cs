using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanBullet : MonoBehaviour

{
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        body.velocity = new Vector2(-5f, 0f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Player")
            Destroy(this.gameObject);
    }
}
