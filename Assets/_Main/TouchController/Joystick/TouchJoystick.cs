using UnityEngine;
using UnityEngine.EventSystems;

public class TouchJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("摇杆边界")] [Tooltip("摇杆的外边框")] public RectTransform outline;

    [Header("摇杆中心")] [Tooltip("摇杆的中心点")] public RectTransform center;

    [Header("摇杆的移动范围")] [Tooltip("控制摇杆的最大半径")] public float maxRadius = 100f;

    [SerializeField] private Vector2 v2_inputVector;

    // 当用户触摸摇杆时触发
    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateJoystickPosition(eventData);
    }

    // 当用户拖动摇杆时触发
    public void OnDrag(PointerEventData eventData)
    {
        UpdateJoystickPosition(eventData);
    }

    // 当用户松开触摸时触发
    public void OnPointerUp(PointerEventData eventData)
    {
        ResetJoystick();
    }

    // 更新摇杆的位置和输入向量
    private void UpdateJoystickPosition(PointerEventData eventData)
    {
        // 获取触摸位置相对于摇杆的Outline的位置
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(outline, eventData.position, eventData.pressEventCamera,
            out localPoint);

        // 限制摇杆中心在最大半径内移动
        if (localPoint.magnitude > maxRadius)
        {
            localPoint = localPoint.normalized * maxRadius;
        }

        // 设置中心位置
        center.anchoredPosition = localPoint;

        // 计算摇杆的输入向量，范围从 -1 到 1
        v2_inputVector = localPoint / maxRadius;
    }

    // 重置摇杆到初始位置
    private void ResetJoystick()
    {
        center.anchoredPosition = Vector2.zero;
        v2_inputVector = Vector2.zero;
    }

    // 返回摇杆的输入向量
    public Vector2 GetInputVector()
    {
        return v2_inputVector;
    }
}