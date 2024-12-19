using UnityEngine;
using System.Collections.Generic;

public class PlayerStatus : CharacterStatus
{
    [System.Serializable]
    public class BattleValues
    {
        [Header("最大法力值")] public int i_maxMP = 100;

        [Header("最大体力值")] public int i_maxSP = 100;

        [Header("等级")] public int level = 1;

        [Header("到下一等级的经验")] public int nextlevelEXP = 1000;

        [Header("技能点")] public int skillpoints = 0;

        [Header("重攻击击破叠加值")] public int heavyattackSG = 20;

        [Header("是否真实伤害")] public bool b_taketruedamage = false;

        [Header("是否是重攻击")] public bool b_isheavyattack = false;

        [Header("攻击是否击飞")] public bool b_isknockup = false;

        [Header("是否在锁定状态")] public bool b_islocking = false;
    }

    [System.Serializable]
    public class ProgramValues
    {
        [Header("攻击顿帧时间")] public float f_attackstoptime = 0.1f;

        [Header("攻击顿帧程度")] public float f_attackstopscale = 0.1f;

        [Header("是否完美防御")] public bool b_isperfectdefence = false;

        [Header("击飞敌人列表")]
        public Dictionary<GameObject, Vector3> d_knockupenemys = new Dictionary<GameObject, Vector3>();

        [Header("武器碰撞体")] public Collider weaponcollider = null;

        [Header("Body碰撞体")] public Collider body_ground_collider = null;

        [Header("BodyAir碰撞体")] public Collider body_air_collider = null;

        [Header("Defence碰撞体")] public Collider defence_collider = null;

        [Header("刚体")] public Rigidbody rb = null;

        [Header("锁定的目标")] public Transform transform_target;

        [Header("RootMotion的Transform")] public Transform transform_rootmotion;

        [Header("是否可以由玩家操作")] public bool b_inputenable = true;
    }

    [System.Serializable]
    public class MoveValues
    {
        [Header("地面物理层")] public LayerMask groundLayer;

        [Header("旋转速度")] public float f_rotationSpeed = 5;

        [Header("是否正在翻滚")] public bool b_isrolling = false;

        [Header("是否在地面上")] public bool b_isontheground = true;

        [Header("移动方向")] public Vector3 v3_targetDirection;

        [Header("锁定时动画机x值")] public float f_movex = 0f;
        [Header("锁定时动画机y值")] public float f_movey = 0f;

        // 足部IK权重
        [Header("IK权重")] [Range(0, 1)] public float ikWeight = 1.0f;
        [Header("脚的位置偏移值")] public float footOffset = 0.1f;
    }


    public BattleValues battlevalues = new BattleValues();
    public ProgramValues programvalues = new ProgramValues();
    public MoveValues movevalues = new MoveValues();


    public void ChangeCollider(Collider _collider)
    {
        Collider[] colliders = new Collider[2] { programvalues.body_ground_collider, programvalues.body_air_collider };
        foreach (Collider collider in colliders)
        {
            // 如果不是当前的Collider，则关闭它
            if (collider != _collider)
            {
                collider.enabled = false;
            }
        }

        _collider.enabled = true;
    }
}