using UnityEngine;

public class EnemyStatus : CharacterStatus
{
    [System.Serializable]
    public class BattleStatus
    {
        [Header("击破值")] public int i_maxSG = 100;

        [Header("是否被击破")] public bool b_isbreak = false;
        
        [Header("是否被击飞")] public bool b_isknockup = false;
    }

    [System.Serializable]
    public class ProgramStatus
    {
        [Header("当前目标")] public GameObject currentTarget = null;

        [Header("刚体")] public Rigidbody rb;

        [Header("动画机")] public Animator animator;
        
        [Header("与玩家距离最小值")] public float min_distancetoplayer = 2f;
        
        [Header("与玩家距离最大值")] public float max_distancetoplayer = 5f;
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