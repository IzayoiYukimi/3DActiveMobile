using System;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    PlayerTouchController playertouchcontroller;

    private Rigidbody rb;

    [SerializeField] LayerMask groundLayer;

    [Tooltip("旋转速度")] [SerializeField] private float f_rotationSpeed = 5;

    [Tooltip("是否正在翻滚")] [SerializeField] private bool b_isrolling = false;

    [Tooltip("是否锁定目标")] [SerializeField] private bool b_islocking = false;

    [Tooltip("是否在地面上")] [SerializeField] private bool b_isontheground = true;

    // 足部IK权重
    [Range(0, 1)] public float ikWeight = 1.0f;
    [Tooltip("脚的位置偏移值")] public float footOffset = 0.1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playertouchcontroller = GetComponent<PlayerTouchController>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsInput();
        SetAnimatorParameters();
        PlayerRoll();
    }

    void PlayerRoll()
    {
        if (playertouchcontroller.b_rollbuttonpressed && !b_isrolling)
        {
            if (b_islocking)
            {
                animator.SetFloat("RollX", playertouchcontroller.v2_inputmove.x);
                animator.SetFloat("RollY", playertouchcontroller.v2_inputmove.y);
            }
            else
            {
                animator.SetFloat("RollX", 0f);
                animator.SetFloat("RollY", 1f);
            }


            animator.SetTrigger("Roll");
            b_isrolling = true;
            playertouchcontroller.ResetRollPressed();
        }
    }

    public void ResetisRolling()
    {
        b_isrolling = false;
    }

    void SetAnimatorParameters()
    {
        animator.SetFloat("Speed", playertouchcontroller.v2_inputmove.magnitude);
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            // 启用IK并设置权重
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, ikWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, ikWeight);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, ikWeight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, ikWeight);

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
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1f, groundLayer))
        {
            footPos = hitInfo.point;
            footPos.y += footOffset; // 加入偏移值使脚离地

            // 设置脚的位置和旋转
            animator.SetIKPosition(foot, footPos);
            animator.SetIKRotation(foot, Quaternion.LookRotation(transform.forward, hitInfo.normal));
        }
    }

    private void RotateTowardsInput()
    {
        // 获取摄像头的前方向（z轴）作为参考方向
        Vector3 cameraForward = playertouchcontroller.transform_camera.forward;
        cameraForward.y = 0f; // 忽略垂直方向，确保只在水平面旋转
        cameraForward.Normalize();

        // 获取摄像头的右方向
        Vector3 cameraRight = playertouchcontroller.transform_camera.right;
        cameraRight.y = 0f; // 忽略垂直方向
        cameraRight.Normalize();

        // 根据摇杆输入计算目标方向
        Vector3 targetDirection = cameraForward * playertouchcontroller.v2_inputmove.y +
                                  cameraRight * playertouchcontroller.v2_inputmove.x;

        // 如果目标方向不为零
        if (targetDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, f_rotationSpeed * Time.deltaTime);
        }
    }
}