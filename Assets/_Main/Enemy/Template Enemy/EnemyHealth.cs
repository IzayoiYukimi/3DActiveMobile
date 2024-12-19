using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]private EnemyStatus enemystatus;

    [SerializeField] int i_currentSG = 0;
    [SerializeField] int i_currentHP = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        i_currentHP = enemystatus.basevalues.i_maxHP;
        i_currentSG = enemystatus.battlestatus.i_maxSG;
    }

    // Update is called once per frame
    void Update()
    {
        if (i_currentHP <= 0 && !enemystatus.basevalues.b_isdead)
        {
            enemystatus.basevalues.b_isdead = true;
        }
    }

    public void TakeDamage(bool _istruedamage, float _ATK, float _AP, int _SG = 10)
    {
        if (_AP >= enemystatus.basevalues.f_defencepower || _istruedamage)
        {
            i_currentHP -= (int)_ATK;
        }
        else
        {
            i_currentHP -= (int)(_ATK * ((100 - (enemystatus.basevalues.f_defencepower - _AP)) / 100));
        }

        i_currentSG -= _SG;
    }

    public void TakeStatus(int _status)
    {
        switch (_status)
        {
            case 1:
                if (!enemystatus.programstatus.animator.GetBool("Knockup"))
                {
                    enemystatus.programstatus.animator.SetTrigger("KnockupTrigger");
                    enemystatus.programstatus.animator.SetBool("Knockup", true);
                }
                break;
        }
    }
}