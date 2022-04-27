using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = -1f;

    private float horizontal = 0f;
    
    private Rigidbody2D rb;

    const float speedXMultiplayer = 50f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speedX * speedXMultiplayer * Time.fixedDeltaTime, rb.velocity.y);
    }
}
