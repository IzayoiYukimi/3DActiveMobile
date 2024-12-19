using UnityEngine;

public class SamuraiMove : MonoBehaviour
{
    SamuraiController samuraicontroller;
    Animator animator;
    Vector3 _targetpos = Vector3.zero;

    private float f_movex = 0.0f;

    private float f_movey = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        samuraicontroller = GetComponent<SamuraiController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _targetpos = samuraicontroller.enemystatus.programstatus.currentTarget.transform.position;
        Move();
        Trun();
    }

    void Move()
    {
        
        float _distance = (_targetpos - transform.position).magnitude;

        if (_distance > samuraicontroller.enemystatus.programstatus.max_distancetoplayer)
        {
            f_movey = 1f;
            Vector3 direction = _targetpos - transform.position;
            direction.y = 0; // 如果需要保持水平旋转，忽略 Y 轴

            // 计算目标旋转
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // 平滑过渡到目标旋转
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                5 * Time.deltaTime);
        }
        else if (_distance < samuraicontroller.enemystatus.programstatus.min_distancetoplayer) f_movey = -1f;
        else
        {
            f_movey = 0f;
            if ((_targetpos - transform.position).x > 5) f_movex = -1f; // 设置远离目标的 X 方向
            else if ((_targetpos - transform.position).x < -5) f_movex = 1f;
            else f_movex = 0f;
        }

        animator.SetFloat("MoveX", Mathf.Lerp(animator.GetFloat("MoveX"), f_movex, Time.deltaTime));
        animator.SetFloat("MoveY", Mathf.Lerp(animator.GetFloat("MoveY"), f_movey, Time.deltaTime));
    }

    void Trun()
    {
        float _angle = Vector3.Angle(_targetpos - transform.position, transform.forward);
        Vector3 _cross = Vector3.Cross(transform.forward, _targetpos - transform.position);
        if (_angle > 45)
        {
            if (_cross.y < 0)
                animator.SetTrigger("TrunLBack");
            else if (_cross.y > 0)
            {
                animator.SetTrigger("TrunRBack");
            }
        }
    }
}