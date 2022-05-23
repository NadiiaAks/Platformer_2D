using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private Animator animator;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioSource playerHitSound;
    [SerializeField] private float recoveryHealth;
    [SerializeField] private float speedRecovery;

    private float _health;

    private void Start()
    {
        _health = totalHealth;
    }

    private void Update()
    {

        healthSlider.value = _health / totalHealth;
        RecoveryHealth();
    }
    public void ReduceHealth(float damage)
    {

        _health -= damage;

        playerHitSound.Play();

        animator.SetTrigger("takeDamage");

        if (_health <= 0)
        {
            Die();
        }
    }

    public void RecoveryHealth()
    {
        if(_health < totalHealth)
        {
            _health += recoveryHealth * speedRecovery;
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
    }
}
