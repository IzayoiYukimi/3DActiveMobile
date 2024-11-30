using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerAnimationEvent : MonoBehaviour
{
    private Animator animator;
    private PlayerMove playermove;
    private PlayerStatus playervalues;

    [SerializeField] List<string> triggers = new List<string>();

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        playermove = GetComponent<PlayerMove>();
        playervalues = GetComponent<PlayerStatus>();

        foreach (var parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                triggers.Add(parameter.name);
            }
        }
    }

    public void OverRoll() //结束翻滚
    {
        playermove.ResetisRolling();
    }

    public void ResetTrigger() //重置所有触发器
    {
        foreach (var trigger in triggers)
        {
            animator.ResetTrigger(trigger);
        }
    }

    public void SetAttackable() //攻击的第一帧
    {
        playervalues.basevalues.b_attackable = true;
    }

    public void ResetAttackable() //攻击结束
    {
        playervalues.basevalues.b_attackable = false;
        if (playervalues.battlevalues.b_isheavyattack) playervalues.battlevalues.b_isheavyattack = false;
    }


    public void Knockup(int _combo = 1) //打飞攻击的第一帧
    {
        playervalues.battlevalues.b_isknockup = true;
        SetAttackable();
        if (_combo == 1)
        {
            ChangeRigidbodyGravity(false);
        }
    }

    public void FinishKnockup(int _combo = 1) //打飞攻击的最后一帧
    {
        playervalues.programvalues.d_knockupenemys.Clear();
        playervalues.battlevalues.b_isknockup = false;
        ResetAttackable();
        if (_combo == 2)
        {
            ChangeRigidbodyGravity(true);
        }
    }

    public void ChangeRigidbodyGravity(bool _enable)
    {
        playervalues.programvalues.rb.useGravity = _enable;
    }
}