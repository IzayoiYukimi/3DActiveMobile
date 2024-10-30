using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerAnimationEvent : MonoBehaviour
{
    private Animator animator;
    private PlayerMove playermove;

    [SerializeField] List<string> triggers = new List<string>();

    private void Start()
    {
        animator = GetComponent<Animator>();
        playermove = GetComponent<PlayerMove>();

        foreach (var parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                triggers.Add(parameter.name);
            }
        }
    }

    public void OverRoll()
    {
        Debug.Log("Reseted!!!");
        playermove.ResetisRolling();
    }

    public void ResetTrigger()
    {
        foreach (var trigger in triggers)
        {
            animator.ResetTrigger(trigger);
        }
    }
}