using System;
using UnityEngine;
using　System.Collections;

public class PlayerWeaponCollider : MonoBehaviour
{
    [SerializeField] private PlayerStatus playervalues;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        playervalues.programvalues.weaponcollider = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void AttackEnemy(Collider _other)
    {
        if (!playervalues.battlevalues.b_isheavyattack)
            _other.GetComponent<EnemyHealth>().TakeDamage(playervalues.battlevalues.b_taketruedamage,
                playervalues.basevalues.f_attackpower, playervalues.basevalues.f_armorpentration);
        else
            _other.GetComponent<EnemyHealth>().TakeDamage(playervalues.battlevalues.b_taketruedamage,
                playervalues.basevalues.f_attackpower, playervalues.basevalues.f_armorpentration,
                playervalues.battlevalues.heavyattackSG);

        StartCoroutine(PerformHitStop());
    }

    void KnockupEnemy(Collider _other)
    {
        if (!playervalues.programvalues.d_knockupenemys.ContainsKey(_other.gameObject))
        {
            playervalues.programvalues.d_knockupenemys.Add(_other.gameObject,
                _other.transform.position - playervalues.transform.position);
            AttackEnemy(_other);
        }
    }

    private IEnumerator PerformHitStop()
    {
        //顿帧
        Time.timeScale = playervalues.programvalues.f_attackstopscale;

        // 暂停的实际时间（受TimeScale影响）
        yield return new WaitForSecondsRealtime(playervalues.programvalues.f_attackstoptime);

        // 恢复时间缩放
        Time.timeScale = 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (playervalues.battlevalues.b_isknockup)
            {
                KnockupEnemy(other);
            }
            else
            {
                AttackEnemy(other);
            }
        }
    }
}