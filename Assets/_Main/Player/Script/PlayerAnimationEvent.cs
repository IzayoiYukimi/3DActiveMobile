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

    public void SetAttackable() //攻击的第一帧
    {
        playervalues.basevalues.b_attackable = true;
    }

    public void ResetAttackable() //攻击结束
    {
        playervalues.basevalues.b_attackable = false;
        if (playervalues.battlevalues.b_isheavyattack) playervalues.battlevalues.b_isheavyattack = false;
    }


    public void Knockup() //打飞攻击的第一帧
    {
        playervalues.battlevalues.b_isknockup = true;
        SetAttackable();
    }

    public void FinishKnockup() //打飞攻击的最后一帧
    {
        playervalues.programvalues.d_knockupenemys.Clear();
        playervalues.battlevalues.b_isknockup = false;
        ResetAttackable();
    }

    public void UnenableRigidbody()
    {
        playervalues.programvalues.rb.isKinematic = true;
    }

    public void IntoIdle()
    {
        foreach (var trigger in triggers)
        {
            animator.ResetTrigger(trigger);
        }
        playervalues.programvalues.rb.isKinematic = false;
        playervalues.ChangeCollider(playervalues.programvalues.body_ground_collider);
        
    }

    public void ChangeAirCollider()
    {
        playervalues.ChangeCollider(playervalues.programvalues.body_air_collider);
        UnenableRigidbody();
    }

    public void SetPerfectDefence(int _timing)
    {
        playervalues.programvalues.b_isperfectdefence = _timing != 0;
    }

    public void SetDefenceCollider(int _enable)
    {
        playervalues.programvalues.defence_collider.enabled = _enable != 0;
    }
}