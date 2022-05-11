using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float walkDistance = 6f;
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float chasingSpeed = 3f;
    [SerializeField] private float timeToWait = 5f;
    [SerializeField] private float timeToChase = 3f;
    [SerializeField] private float minDistanceToPlayer = 1.5f;

    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private Vector2 _leftPosition;
    private Vector2 _rightPosition;
    private Vector2 _nextPoint;

    private bool _isFacingRight = true;
    private bool _isWait = false;
    private bool _isChasingPlayer;

    private float _waitTime;
    private float _chaseTime;
    private float _walkSpeed;

    public bool IsFacingRifht
    {
        get => _isFacingRight;
    }

    public void StartChasingPlayer()
    {
        _isChasingPlayer = true;
        _chaseTime = timeToChase;
        _walkSpeed = chasingSpeed;
    }

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>(); 
        _leftPosition = transform.position;
        _rightPosition = _leftPosition + Vector2.right * walkDistance;
        _waitTime = timeToWait;
        _chaseTime = timeToChase;
        _walkSpeed = patrolSpeed;
    }

    private void Update()
    {
        if (_isChasingPlayer)
        {
            StartChasingTimer();
        }

        if(_isWait && !_isChasingPlayer)
        {
            StartWaitTimer();
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
        _nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;

        if(_isChasingPlayer && Mathf.Abs(DistanceToPlayer()) < minDistanceToPlayer)
        {
            return;
        }

        if (_isChasingPlayer)
        {
            ChasePlayer();
        }
        
        if (!_isWait && !_isChasingPlayer)
        {
            Patrol();
        }
    }

    private void ChasePlayer()
    {
        float distance = DistanceToPlayer();
        if(distance < 0)
        {
            _nextPoint.x *= -1;
        }

        if(distance > 0.2f && !_isFacingRight)
        {
            Flip();
        } else if(distance < 0.2f && _isFacingRight)
        {
            Flip();
        }

        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private float DistanceToPlayer()
    {
        return _playerTransform.position.x - transform.position.x;
    }

    private void Patrol()
    {
        if (!_isFacingRight)
        {
            _nextPoint.x *= -1;
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftPosition, _rightPosition);
    }

   //waiting (timer code)
   private void StartWaitTimer ()
    {
        _waitTime -= Time.deltaTime;
        if(_waitTime < 0f)
        {
            _waitTime = timeToWait;
            _isWait = false;
            Flip();
        }
    }

    private void StartChasingTimer()
    {
        _chaseTime -= Time.deltaTime;

        if( _chaseTime < 0f)
        {
            _isChasingPlayer = false;
            _chaseTime = timeToChase;
            _walkSpeed = patrolSpeed;
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
