using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int i_maxHP = 100;
    [SerializeField] private int i_currentHP = 0;

    [SerializeField] private bool b_isdead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        i_currentHP = i_maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (i_currentHP <= 0 && !b_isdead)
        {
            b_isdead = true;
        }
    }

    public void TakeDamage(int _damage)
    {
        i_currentHP -= _damage;
    }
}