using System;
using UnityEngine;

public class EnableCollider : MonoBehaviour
{
    public bool b_isenable = false;
    [SerializeField] EnemyStatus enemystatus;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemystatus.programstatus.currentTarget = other.transform.root.gameObject;
            enemystatus.programstatus.currentTarget.GetComponent<PlayerStatus>().programvalues.b_inputenable = false;
            b_isenable = true;
        }
    }
}
