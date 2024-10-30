using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButton : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] bool b_ispressed = false;
    [SerializeField] bool b_isdraged = false;

    [SerializeField] private Vector2 v2_dragbeginpos;
    [SerializeField] private Vector2 v2_dragendpos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool GetisPressed()
    {
        return b_ispressed;
    }

    public void Press()
    {
        b_ispressed = true;
    }

    public void Release()
    {
        b_ispressed = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        v2_dragbeginpos = eventData.position;
        v2_dragendpos = eventData.position;
    }

    // 手指抬起时的事件
    public void OnEndDrag(PointerEventData eventData)
    {
        v2_dragendpos = eventData.position;
        b_isdraged = true;
    }

    public bool GetDraged()
    {
        return b_isdraged;
    }
    public void ResetDraged()
    {
        b_isdraged = false;
    }

    // 检测滑动方向
    public Vector2 GetDragDirection()
    {
        Vector2 v2_dragdirection = v2_dragendpos - v2_dragbeginpos;

        // 如果滑动距离太短，则不触发任何事件
        if (v2_dragdirection.magnitude < 50) return Vector2.zero;
        else return v2_dragdirection;
    }
}