using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerTouchController playerTouchController;
    PlayerValues playervalues;

    Animator animator;

    [SerializeField] float f_attackbuttonpressedtime = 0f;
    private bool b_attacked = false;

    [SerializeField] bool b_islocking = false;

    public Transform transform_target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        playerTouchController = GetComponent<PlayerTouchController>();
        playervalues = GetComponent<PlayerValues>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        CheckSkill();

        b_islocking = playerTouchController.b_islocking;
    }

    void Attack()
    {
        if (playerTouchController.b_attackbuttonpressed)
        {
            b_attacked = true;
            f_attackbuttonpressedtime += Time.deltaTime;
        }
        else
        {
            if (b_attacked)
            {
                if (f_attackbuttonpressedtime < 0.3f)
                {
                    animator.SetTrigger("Attack");
                }
                else
                {
                    animator.SetTrigger("HeavyAttack");
                    playervalues.b_isheavyattack = true;
                }

                b_attacked = false;
                f_attackbuttonpressedtime = 0f;
            }
        }
    }

    void CheckSkill()
    {
        if (playerTouchController.v2_attackbuttondrag != Vector2.zero)
        {
            // 判断滑动方向
            if (Mathf.Abs(playerTouchController.v2_attackbuttondrag.x) >
                Mathf.Abs(playerTouchController.v2_attackbuttondrag.y))
            {
                // 水平滑动
                if (playerTouchController.v2_attackbuttondrag.x > 0)
                    OnDrapRight();
                else
                    OnDrapLeft();
            }
            else
            {
                // 垂直滑动
                if (playerTouchController.v2_attackbuttondrag.y > 0)
                    OnDrapUp();
                else
                    OnDrapDown();
            }
        }
    }


    void OnDrapRight()
    {
    }

    void OnDrapLeft()
    {
    }

    void OnDrapUp()
    {
        if (playerTouchController.b_attackbuttondraged)
        {
            animator.SetTrigger("BackSpin");

            playerTouchController.ResetAttackDrag();
        }
    }

    void OnDrapDown()
    {
    }

    public bool Getislocking()
    {
        return b_islocking;
    }

    public Transform GetTarget()
    {
        return transform_target;
    }
}