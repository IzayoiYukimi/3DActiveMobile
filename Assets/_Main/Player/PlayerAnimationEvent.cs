using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerAnimationEvent : MonoBehaviour
{
    private Animator animator;
    private PlayerMove playermove;
    private PlayerValues playervalues;

    [SerializeField] List<string> triggers = new List<string>();

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        playermove = GetComponent<PlayerMove>();
        playervalues = GetComponent<PlayerValues>();

        foreach (var parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                triggers.Add(parameter.name);
            }
        }
    }

    public void OverRoll()//结束翻滚
    {
        playermove.ResetisRolling();
    }
    
    public void ResetTrigger()//重置所有触发器
    {
        foreach (var trigger in triggers)
        {
            animator.ResetTrigger(trigger);
        }
    }

    public void SetAttackable()//攻击的第一帧
    {
        playervalues.b_attackable = true;
    }

    public void ResetAttackable()//攻击结束
    {
        playervalues.b_attackable = false;
        playervalues.b_isheavyattack = false;
    }
}