using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pidgeon : MonoBehaviour
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DoRandomAction()
    {
        int randomAction = Random.Range(0,4);
        animator.SetInteger("RandomAnimation", randomAction);
    }
}
