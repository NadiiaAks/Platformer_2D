using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArm : MonoBehaviour
{
    private Finish _finish;
    [SerializeField] Animator animator;
    private void Start()
    {
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
    }

    public void ActivateLevelArm()
    {
        animator.SetTrigger("activate");
        _finish.Activate();
    }
}
