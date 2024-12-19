using System;
using UnityEngine;

public class SamuraiAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField]private EnemyStatus enemystatus;
    [SerializeField]private Animator animator;

    private void OnEnable()
    {
        enemystatus = GetComponent<SamuraiStatus>();
        animator = GetComponent<Animator>();
    }

    public void EquipWeapon()
    {
        weapon.SetActive(true);
    }

    public void FinishPerformance()
    {
        enemystatus.programstatus.currentTarget.GetComponent<PlayerStatus>().programvalues.b_inputenable = true;
        enemystatus.programstatus.rb.isKinematic = false;
    }

    public void ResetTrigger(string _triggername)
    {
        animator.ResetTrigger(_triggername);
    }
}
