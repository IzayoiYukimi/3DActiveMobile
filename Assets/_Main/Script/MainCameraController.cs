using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [Header("目标对象")] public Transform target;

    [Header("旋转速度")] public float f_rotationSpeed = 0.2f;
    public float f_smoothSpeed = 5f;

    [Header("摄像头距离")] public float f_distance = 5.0f;

    [Header("垂直角度限制")] public float f_minVerticalAngle = -0f;
    public float f_maxVerticalAngle = 60f;

    [SerializeField] private AttackButton s_attackbutton;
    [SerializeField] private PlayerStatus playervalues;

    private Vector3 v3_offset;
    private float f_currentYaw = 0f;
    private float f_currentPitch = 0f;

    void OnEnable()
    {
        v3_offset = new Vector3(0, 0, -f_distance);
        transform.position = target.position + v3_offset;
        transform.LookAt(target);
    }

    void Update()
    {
        if (playervalues.battlevalues.b_islocking)
        {
            // 锁定状态下持续保持相机在延长线上
            MaintainCameraOnExtensionLine();
        }
        else
        {
            // 未锁定时的常规摄像机控制（例如自由旋转）
            HandleFreeCamera();
        }
    }

    void MaintainCameraOnExtensionLine()
    {
        // 计算角色到目标的方向向量
        Vector3 directionToTarget = (target.position - playervalues.programvalues.transform_target.position).normalized;

        // 计算相机的位置，使其在目标到角色的延长线上
        Vector3 desiredPosition = target.position + directionToTarget * f_distance;

        // 平滑移动摄像机到目标位置
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * f_smoothSpeed);

        // 让摄像机朝向目标
        Quaternion targetRotation = Quaternion.LookRotation(playervalues.programvalues.transform_target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * f_smoothSpeed);
    }

    void HandleFreeCamera()
    {
        // 检查触摸输入并更新摄像头位置
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2 && !s_attackbutton.GetisPressed())
            {
                HandleTouchRotation(touch);
            }
        }

        UpdateCameraPosition();
    }


    private void HandleTouchRotation(Touch touch)
    {
        if (touch.phase == TouchPhase.Moved)
        {
            float deltaX = touch.deltaPosition.x * f_rotationSpeed;
            float deltaY = -touch.deltaPosition.y * f_rotationSpeed;
            f_currentYaw += deltaX;
            f_currentPitch += deltaY;
            f_currentPitch = Mathf.Clamp(f_currentPitch, f_minVerticalAngle, f_maxVerticalAngle);
        }
    }

    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(f_currentPitch, f_currentYaw, 0);
        Vector3 rotatedOffset = rotation * v3_offset;
        transform.position = target.position + rotatedOffset;
        transform.LookAt(target);
    }
}