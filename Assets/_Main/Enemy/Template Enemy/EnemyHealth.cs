using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyValues enemyvalues;

    [SerializeField] int i_currentSG = 0;
    [SerializeField] int i_currentHP = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        enemyvalues = GetComponent<EnemyValues>();
        i_currentHP = enemyvalues.i_maxHP;
        i_currentSG = enemyvalues.i_maxSG;
    }

    // Update is called once per frame
    void Update()
    {
        if (i_currentHP <= 0&&!enemyvalues.b_isdead)
        {
            enemyvalues.b_isdead = true;
        }
    }

    public void TakeDamage(bool _istruedamage, float _ATK, float _AP, int _SG = 10)
    {
        if (_AP >= enemyvalues.f_defencepower || _istruedamage)
        {
            i_currentHP -= (int)_ATK;
        }
        else
        {
            i_currentHP -= (int)(_ATK * ((100 - (enemyvalues.f_defencepower - _AP)) / 100));
        }

        i_currentSG -= _SG;
    }
    
}