using UnityEngine;

public class EnemyStatus : CharacterStatus
{
    [System.Serializable]
    public class BattleStatus
    {
        [Header("击破值")] public int i_maxSG = 100;

        [Header("是否被击破")] public bool b_isbreak = false;
    }

    [System.Serializable]
    public class ProgramStatus
    {
        [Header("当前目标")] public GameObject currentTarget = null;

        [Header("刚体")] public Rigidbody rb;

        [Header("动画机")] public Animator animator;
    }

    [System.Serializable]
    public class MoveStatus
    {
        [Header("是否在地面上")] public bool b_isontheground = true;
    }

    public BattleStatus battlestatus = new BattleStatus();
    public ProgramStatus programstatus = new ProgramStatus();
    public MoveStatus movestatus = new MoveStatus();
}