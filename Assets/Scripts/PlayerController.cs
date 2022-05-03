using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = -1f;
    [SerializeField] private float speedJump = 400f;
    [SerializeField] private Animator animator;

    private float horizontal = 0f;
    private bool isFacingRight = true;

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
        horizontal = Input.GetAxis("Horizontal"); //-1:1
        animator.SetFloat("speedX", Mathf.Abs(horizontal));
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
            rb.AddForce(new Vector2(0f, speedJump));
            isGround = false;
            isJump = false;
        }

        if (horizontal > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (horizontal < 0f && isFacingRight)  
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 playerFlip = transform.localScale;
        playerFlip.x *= (-1);
        transform.localScale = playerFlip;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Worked"); 
        }
    }
}
