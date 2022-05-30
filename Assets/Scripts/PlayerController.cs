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
    [SerializeField] private FixedJoystick fixedJoystick;

    private float _horizontal = 0f;
    private bool _isFacingRight = true;

    private bool _isGround = false;
    private bool _isJump = false;
    private bool _isFinish = false;
    private bool _isLevelArm = false;
    
    private Rigidbody2D _rb;
    private Finish _finish;
    private LevelArm _levelArm;

    const float _speedXMultiplayer = 50f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
        _levelArm = FindObjectOfType<LevelArm>();
    }

    private void Update()
    {
        //Mobile moving
        _horizontal = fixedJoystick.Horizontal;

        //PC moving
        //_horizontal = Input.GetAxis("Horizontal"); //-1:1

        animator.SetFloat("speedX", Mathf.Abs(_horizontal));

        //PC jumping
        /*if(Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }*/

        if (Input.GetKeyDown(KeyCode.F))
        {
            if(_isFinish)
            {
                _finish.FinishLevel();
            }
            if (_isLevelArm)
            {
                _levelArm.ActivateLevelArm();
            }
        }
        
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speedX * _speedXMultiplayer * Time.fixedDeltaTime, _rb.velocity.y);

        if (_isJump)
        {
            _rb.AddForce(new Vector2(0f, speedJump));
            _isGround = false;
            _isJump = false;
        }

        if (_horizontal > 0f && !_isFacingRight)
        {
            Flip();
        }
        else if (_horizontal < 0f && _isFacingRight)  
        {
            Flip();
        }
    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerFlip = playerModelTransform.localScale;
        playerFlip.x *= (-1);
        playerModelTransform.localScale = playerFlip;
    }

    public void Jump()
    {
        if (_isGround)
        {
            _isJump = true;
            jumpSound.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LevelArm levelArmTemp = other.GetComponent<LevelArm>();

        if (other.CompareTag("Finish"))
        {
            Debug.Log("Worked");
            _isFinish = true;
        }

        if (levelArmTemp != null)
        {
            _isLevelArm = true;
        }
    }
    private void OnTriggerExit2D (Collider2D other)
    {
        LevelArm levelArmTemp = other.GetComponent<LevelArm>();

        if (other.CompareTag("Finish"))
        {
            Debug.Log("Not Worked");
            _isFinish = false;
        }

        if (levelArmTemp != null)
        {
            _isLevelArm = false;
        }
    }

}
