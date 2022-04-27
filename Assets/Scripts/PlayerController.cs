using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = -1f;

    private float horizontal = 0f;

    private bool isGround = false;
    private bool isJump = false;
    
    private Rigidbody2D rb;

    const float speedXMultiplayer = 50f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if(Input.GetKey(KeyCode.W) && isGround)
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speedX * speedXMultiplayer * Time.fixedDeltaTime, rb.velocity.y);
        if (isJump)
        {
            rb.AddForce(new Vector2(0f, 400f));
            isGround = false;
            isJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
