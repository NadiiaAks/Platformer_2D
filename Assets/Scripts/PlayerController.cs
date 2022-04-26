using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = -1f;
    
    Rigidbody2D rb;

    const float speedXMultiplayer = 50f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixUpdate()
    {
        rb.velocity = Vector2(speedX * speedXMultiplayer * Time.fixedDeltaTime, rb.velocity.y);
    }
}
