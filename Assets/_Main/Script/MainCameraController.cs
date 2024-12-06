using Unity.Cinemachine;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [Header("旋转速度")] public float f_rotationSpeed = 0.2f;

    [Header("摄像头距离")] public float f_distance = 5.0f;

    [Header("垂直角度限制")] public float f_minVerticalAngle = 0f;
    public float f_maxVerticalAngle = 60f;

    [SerializeField] private AttackButton s_attackbutton;

    [SerializeField] private CinemachineCamera cinemachinecamera;

    public Vector3 v3_offset = new Vector3(0.0f, 0.0f, 0.0f);
    private float f_currentYaw = 0f;
    private float f_currentPitch = 0f;

    void OnEnable()
    {
        f_distance = (cinemachinecamera.Target.TrackingTarget.position - transform.position).magnitude;
    }

    void Update()
    {
        // 未锁定时的常规摄像机控制（例如自由旋转）
        HandleFreeCamera();
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
        transform.position = cinemachinecamera.Target.TrackingTarget.position + rotatedOffset;
        transform.LookAt(cinemachinecamera.Target.TrackingTarget);
    }
}