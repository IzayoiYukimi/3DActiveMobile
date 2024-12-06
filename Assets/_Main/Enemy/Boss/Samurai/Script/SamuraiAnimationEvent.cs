using System;
using UnityEngine;

public class SamuraiAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    private EnemyStatus enemystatus;

    private void OnEnable()
    {
        enemystatus = GetComponent<EnemyStatus>();
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
}
