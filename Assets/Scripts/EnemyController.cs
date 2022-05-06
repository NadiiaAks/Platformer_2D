using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float timeToWait = 5f;

    private Rigidbody2D _rb;
    private Vector2 _leftPosition;
    private Vector2 _rightPosition;

    private bool _isFacingRight = true;
    private bool _isWait = false;
    private float _waitTime;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _leftPosition = transform.position;
        _rightPosition = _leftPosition + Vector2.right * walkDistance;
        _waitTime = timeToWait;
    }

    private void Update()
    {
        if(_isWait)
        {
            Wait();
        }
        
        bool isOutOfRightBoundary = _isFacingRight && transform.position.x >= _rightPosition.x;
        bool isOutOfLeftBoundary = !_isFacingRight && transform.position.x <= _leftPosition.x;

        if(isOutOfRightBoundary || isOutOfLeftBoundary)
        {
            _isWait = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 nextPoint = Vector2.right * walkSpeed * Time.fixedDeltaTime;

        if (!_isFacingRight)
        {
            nextPoint.x *= -1;
        }
        
        if (!_isWait)
        {
            _rb.MovePosition((Vector2)transform.position + nextPoint);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftPosition, _rightPosition);
    }

   //waiting (timer code)
   private void Wait ()
    {
        _waitTime -= Time.deltaTime;
        if(_waitTime < 0f)
        {
            _waitTime = timeToWait;
            _isWait = false;
            Flip();
        }
    }
    
    //flipping character
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 enemyFlip = transform.localScale;
        enemyFlip.x *= (-1);
        transform.localScale = enemyFlip;
    }
}
