using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = -1f;
    [SerializeField] private float speedJump = 400f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private AudioSource jumpSound;

    private float horizontal = 0f;
    private bool isFacingRight = true;

    private bool isGround = false;
    private bool isJump = false;
    private bool isFinish = false;
    private bool isLevelArm = false;
    
    private Rigidbody2D rb;
    private Finish finish;
    private LevelArm levelArm;

    const float speedXMultiplayer = 50f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        levelArm = FindObjectOfType<LevelArm>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal"); //-1:1
        animator.SetFloat("speedX", Mathf.Abs(horizontal));

        if(Input.GetKeyDown(KeyCode.W) && isGround)
        {
            isJump = true;
            jumpSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(isFinish)
            {
                finish.FinishLevel();
            }
            if (isLevelArm)
            {
                levelArm.ActivateLevelArm();
            }
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
        Vector3 playerFlip = playerModelTransform.localScale;
        playerFlip.x *= (-1);
        playerModelTransform.localScale = playerFlip;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LevelArm levelArmTemp = other.GetComponent<LevelArm>();

        if (other.CompareTag("Finish"))
        {
            Debug.Log("Worked");
            isFinish = true;
        }

        if (levelArmTemp != null)
        {
            isLevelArm = true;
        }
    }
    private void OnTriggerExit2D (Collider2D other)
    {
        LevelArm levelArmTemp = other.GetComponent<LevelArm>();

        if (other.CompareTag("Finish"))
        {
            Debug.Log("Not Worked");
            isFinish = false;
        }

        if (levelArmTemp != null)
        {
            isLevelArm = false;
        }
    }

}
