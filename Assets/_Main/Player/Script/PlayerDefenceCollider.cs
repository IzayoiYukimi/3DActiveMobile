using System;
using UnityEngine;

public class PlayerDefenceCollider : MonoBehaviour
{
    public PlayerStatus playerStatus;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            
        }
    }
}
