using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    [SerializeField] private AudioSource hitEnemySound;

    private AttackController _attackController;

    private void Start()
    {
        _attackController = transform.root.GetComponent<AttackController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        if(enemyHealth != null && _attackController.IsAttack)
        {
            hitEnemySound.Play();
            enemyHealth.ReduceHealth(damage);
        }
    }
}
