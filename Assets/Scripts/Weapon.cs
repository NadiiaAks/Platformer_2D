using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private AttackController _attackController;

    private void Start()
    {
        _attackController = transform.root.GetComponent<AttackController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemyController = other.GetComponent<EnemyController>();

        if(enemyController != null && _attackController.IsAttack)
        {
            Debug.Log("Hit");
        }
    }
}
