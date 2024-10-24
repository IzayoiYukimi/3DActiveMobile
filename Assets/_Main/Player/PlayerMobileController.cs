
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMobileController : MonoBehaviour
{
    
    
    
    public Transform cameraTransform; // 摄像机的 Transform，通过外部设置
    public float moveSpeed = 5.0f; // 角色移动速度
    public float rotationSpeed = 120.0f; // 摄像机旋转速度
    public float distance = 5.0f; // 摄像机距离角色的距离
    public float yMinLimit = 0f; // 垂直旋转的最小角度
    public float yMaxLimit = 60f;  // 垂直旋转的最大角度
    
    private float x = 0.0f; // 水平旋转角度
    private float y = 0.0f; // 垂直旋转角度
    private Vector2 lastTouchPosition; // 上一次右半屏触摸的位置
    private Animator animator;

    void Start()
    {
        // 初始化摄像机的角度
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Touchscreen.current != null)
        {
            foreach (var touch in Touchscreen.current.touches)
            {
                if (touch.press.isPressed)
                {
                    // 获取触摸位置
                    Vector2 touchPosition = touch.position.ReadValue();

                    // 判断触摸点在哪个屏幕区域
                    if (touchPosition.x < Screen.width / 2) 
                    {
                        // 左半屏控制移动
                        MovePlayer(touch);
                    }
                    else
                    {
                        // 右半屏控制旋转
                        RotateCamera(touch);
                    }
                }
            }
        }
    }

    // 控制角色移动
    private void MovePlayer(TouchControl touch)
    {
        // 获取触摸的移动变化量
        Vector2 deltaTouch = touch.delta.ReadValue();
        
        // 如果没有输入，就直接退出，不移动
        if (deltaTouch == Vector2.zero)
        {
            // 没有输入时，设置 Animator 参数 isMove 为 false
            animator.SetBool("isMove", false);
            return;
        }

        // 有输入时，设置 Animator 参数 isMove 为 true
        animator.SetBool("isMove", true);

        // 计算移动方向，Y轴方向保持为0，角色不会上升或下降
        Vector3 direction = new Vector3(deltaTouch.x, 0, deltaTouch.y).normalized;

        // 获取角色当前的朝向
        Vector3 forward = transform.forward;

        // 使用 Quaternion 来逐渐旋转角色朝向目标方向
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // 控制摄像机旋转
    private void RotateCamera(TouchControl touch)
    {
        Vector2 touchPosition = touch.position.ReadValue();

        if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
        {
            // 记录触摸开始时的位置
            lastTouchPosition = touchPosition;
        }
        else if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
        {
            // 计算触摸移动差异
            Vector2 deltaTouch = touchPosition - lastTouchPosition;
            lastTouchPosition = touchPosition;

            // 根据触摸移动的差异，更新摄像机旋转角度
            x += deltaTouch.x * rotationSpeed * Time.deltaTime;
            y -= deltaTouch.y * rotationSpeed * Time.deltaTime;

            // 限制垂直旋转角度
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            // 计算新的摄像机旋转
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // 计算摄像机的新位置
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + transform.position;

            // 更新摄像机的位置和旋转
            cameraTransform.rotation = rotation;
            cameraTransform.position = position;
        }
    }
}
