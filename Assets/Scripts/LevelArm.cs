using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArm : MonoBehaviour
{
    private Finish _finish;
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _finish = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
    }

    public void ActivateLevelArm()
    {
        _animator.SetTrigger("activate");
        _finish.Activate();
    }
}
