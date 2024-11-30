using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [System.Serializable]
    public class Basevalues
    {
        [Header("攻击力")] public float f_attackpower = 10;

        [Header("防御力（百分比）")] public float f_defencepower = 0;

        [Header("防御忽视（百分比）")] public float f_armorpentration = 0;

        [Header("最大生命值")] public int i_maxHP = 100;

        [Header("武器是否可以造成伤害")] public bool b_attackable = false;

        [Header("是否免疫伤害")] public bool b_invunerability = false;

        [Header("是否存活")] public bool b_isdead = false;
    }

    public Basevalues basevalues = new Basevalues();
}