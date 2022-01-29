using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void GetDamage()
    {
        animator.SetTrigger("GetDamage");
    }
}
