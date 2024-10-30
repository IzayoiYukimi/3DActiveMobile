using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [Header("目标对象")] [Tooltip("摄像头围绕旋转的目标（通常是Player）")]
    public Transform target;

    [Header("旋转速度")] [Tooltip("控制摄像头旋转的速度")]
    public float f_rotationSpeed = 0.2f;

    [Header("摄像头距离")] [Tooltip("摄像头与目标之间的距离")]
    public float f_distance = 5.0f;

    [Header("垂直角度限制")] [Tooltip("摄像头垂直旋转角度的最小和最大值")]
    public float f_minVerticalAngle = -0f;

    public float f_maxVerticalAngle = 60f;

    [SerializeField] private AttackButton s_attackbutton;

    private Vector3 v3_offset;
    private float f_currentYaw = 0f; // 水平角度
    private float f_currentPitch = 0f; // 垂直角度

    void OnEnable()
    {
        // 初始化摄像头的偏移位置
        v3_offset = new Vector3(0, 0, -f_distance);

        // 初始化摄像头位置
        transform.position = target.position + v3_offset;
        transform.LookAt(target);
    }

    void Update()
    {
        // 检查是否有触摸输入
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 如果触摸在屏幕右半边
            if (touch.position.x > Screen.width / 2  && !s_attackbutton.GetisPressed())
            {
                HandleTouchRotation(touch);
            }
        }

        // 更新摄像头位置和朝向
        UpdateCameraPosition();
    }

    private void HandleTouchRotation(Touch touch)
    {
        if (touch.phase == TouchPhase.Moved)
        {
            // 根据触摸的滑动调整摄像头的旋转角度
            float deltaX = touch.deltaPosition.x * f_rotationSpeed;
            float deltaY = -touch.deltaPosition.y * f_rotationSpeed;

            // 更新水平和垂直角度
            f_currentYaw += deltaX;
            f_currentPitch += deltaY;

            // 限制垂直角度在指定范围内
            f_currentPitch = Mathf.Clamp(f_currentPitch, f_minVerticalAngle, f_maxVerticalAngle);
        }
    }

    private void UpdateCameraPosition()
    {
        // 计算旋转后的摄像头位置
        Quaternion rotation = Quaternion.Euler(f_currentPitch, f_currentYaw, 0);
        Vector3 rotatedOffset = rotation * v3_offset;

        // 更新摄像头的位置和朝向
        transform.position = target.position + rotatedOffset;
        transform.LookAt(target);
    }
}