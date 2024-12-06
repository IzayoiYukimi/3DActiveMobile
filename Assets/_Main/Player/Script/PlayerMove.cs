using System;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerMove : MonoBehaviour
{
    PlayerStatus playervalues;
    Animator animator;
    PlayerTouchController s_playertouchcontroller;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        s_playertouchcontroller = GetComponent<PlayerTouchController>();
        playervalues = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsInput();
        SetAnimatorParameters();
        PlayerRoll();
        CheckDistanceToGround();
    }

    void PlayerRoll()
    {
        if (!playervalues.battlevalues.b_islocking)
        {
            animator.SetFloat("RollX", 0f);
            animator.SetFloat("RollY", 1f);
        }
        else
        {
            if (s_playertouchcontroller.v2_inputmove == Vector2.zero)
            {
                animator.SetFloat("RollX", 0f);
                animator.SetFloat("RollY", 1f);
            }
            else
            {
                animator.SetFloat("RollX", playervalues.movevalues.f_movex);
                animator.SetFloat("RollY", playervalues.movevalues.f_movey);
            }
        }

        if (s_playertouchcontroller.b_rollbuttonpressed && !playervalues.movevalues.b_isrolling)
        {
            animator.SetTrigger("Roll");
            playervalues.movevalues.b_isrolling = true;
            s_playertouchcontroller.ResetRollPressed();
        }
    }

    private void CheckDistanceToGround()
    {
        // 从物体的当前位置向下发射射线
        Ray ray = new Ray(playervalues.programvalues.transform_rootmotion.position, Vector3.down);
        RaycastHit hit;

        // 检测与 Ground Layer 的碰撞
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, playervalues.movevalues.groundLayer))
        {
            if (hit.distance < 0.88f)
            {
                playervalues.movevalues.b_isontheground = true;
                playervalues.ChangeCollider(playervalues.programvalues.body_ground_collider);
            }
            else
            {
                playervalues.movevalues.b_isontheground = false;
                playervalues.ChangeCollider(playervalues.programvalues.body_air_collider);
            }
        }
    }

    //在PlayerAnimationEvent Sprict中调用
    public void ResetisRolling()
    {
        playervalues.movevalues.b_isrolling = false;
    }

    void SetAnimatorParameters()
    {
        animator.SetFloat("Speed", s_playertouchcontroller.v2_inputmove.magnitude);
        animator.SetBool("IsLocking", playervalues.battlevalues.b_islocking);
        animator.SetFloat("MoveX", playervalues.movevalues.f_movex);
        animator.SetFloat("MoveY", playervalues.movevalues.f_movey);
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            // 启用IK并设置权重
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, playervalues.movevalues.ikWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, playervalues.movevalues.ikWeight);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, playervalues.movevalues.ikWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, playervalues.movevalues.ikWeight);

            // 左脚IK位置和旋转
            SetFootIK(AvatarIKGoal.LeftFoot);

            // 右脚IK位置和旋转
            SetFootIK(AvatarIKGoal.RightFoot);
        }
    }

    private void SetFootIK(AvatarIKGoal foot)
    {
        // 获取脚的目标位置
        Vector3 footPos = animator.GetIKPosition(foot);
        Ray ray = new Ray(footPos + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1f, playervalues.movevalues.groundLayer))
        {
            footPos = hitInfo.point;
            footPos.y += playervalues.movevalues.footOffset; // 加入偏移值使脚离地

            // 设置脚的位置和旋转
            animator.SetIKPosition(foot, footPos);
            animator.SetIKRotation(foot, Quaternion.LookRotation(transform.forward, hitInfo.normal));
        }
    }

    private void RotateTowardsInput()
    {
        // 获取摄像头的前方向（z轴）作为参考方向
        Vector3 cameraForward = s_playertouchcontroller.transform_camera.forward;
        cameraForward.y = 0f; // 忽略垂直方向，确保只在水平面旋转
        cameraForward.Normalize();

        // 获取摄像头的右方向
        Vector3 cameraRight = s_playertouchcontroller.transform_camera.right;
        cameraRight.y = 0f; // 忽略垂直方向
        cameraRight.Normalize();

        playervalues.movevalues.v3_targetDirection = (cameraRight * s_playertouchcontroller.v2_inputmove.x) +
                                                     (cameraForward * s_playertouchcontroller.v2_inputmove.y);

        // 基于相机方向计算摇杆输入的世界空间方向
        if (playervalues.battlevalues.b_islocking)
        {
            if (playervalues.programvalues.transform_target != null)
                playervalues.movevalues.v3_targetDirection =
                    (playervalues.programvalues.transform_target.position - transform.position).normalized;
            // 使用计算得到的输入方向来设置移动方向参数
            playervalues.movevalues.f_movex = s_playertouchcontroller.v2_inputmove.x;
            playervalues.movevalues.f_movey = s_playertouchcontroller.v2_inputmove.y;
        }


        // 如果目标方向不为零
        if (playervalues.movevalues.v3_targetDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playervalues.movevalues.v3_targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                playervalues.movevalues.f_rotationSpeed * Time.deltaTime);
        }
    }
}