using System;
using UnityEngine;

public class SamuraiController : MonoBehaviour
{
    [SerializeField] private EnableCollider enablecollider;

    

    [SerializeField] private SamuraiPerformance samuraiperformance;
    
    public EnemyStatus enemystatus;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        enemystatus = GetComponent<EnemyStatus>();
    }


    // Update is called once per frame
    void Update()
    {
        if(enablecollider.b_isenable)ActivateSamurai();
    }


    void ActivateSamurai()
    {
        samuraiperformance.enabled = true;
        enablecollider.gameObject.SetActive(false);
    }
}
