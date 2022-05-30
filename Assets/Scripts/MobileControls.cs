using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
   [SerializeField] private AttackController attackController;

    public void Attack()
    {
        attackController.Attack();
    }
}
