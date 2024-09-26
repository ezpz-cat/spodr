using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    [SerializeField] private Vector2 gravityAmount;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;



    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        body.velocity += gravityAmount;
        
        anim.SetFloat("yVelocity", body.velocity.y);
        
        float verticalMag = body.velocity.y / body.velocity.magnitude;
        float horizontalInput = Input.GetAxis("Horizontal");
        
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
  
        anim.SetBool("grounded", grounded);
        anim.SetBool("run", horizontalInput != 0);

        if (horizontalInput > 0f)
            anim.SetInteger("Direction" , 0);

        else if (horizontalInput < 0f)
            anim.SetInteger("Direction" , 1);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

    }

    private void Jump()
    {

     body.velocity = new Vector2(body.velocity.x, jump);
        grounded = false;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }


}
