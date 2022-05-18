using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private Animator animator;
    [SerializeField] private Slider healthSlider;

    private float _health;

    private void Start()
    {
        _health = totalHealth;
    }

    private void Update()
    {

        healthSlider.value = _health / totalHealth;
    }
    public void ReduceHealth(float damage)
    {

        _health -= damage;

        animator.SetTrigger("takeDamage");

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
