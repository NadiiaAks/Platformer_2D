using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private Slider healthEnemy;
    [SerializeField] private Animator _animator;

    private float _health;

    public void Start()
    {
        _health = totalHealth;
    }
    private void Update()
    {
        healthEnemy.value = _health / totalHealth;
    }

    public void ReduceHealth(float damage)
    {

        Debug.Log("HitEnemy");
        _health -= damage;
        _animator.SetTrigger("takeDamage");

        if(_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
