using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] PlayerStatus playerstatus;

    [SerializeField] private GameObject touchcontroller;

    [SerializeField] private TouchJoystick s_touchjoystick;

    [SerializeField] private AttackButton s_attackbutton;

    [SerializeField] private RollButton s_rollbutton;

    [SerializeField] private LockButton s_lockbutton;

    [SerializeField] private DefenceButton s_defencebutton;

    [SerializeField] private Animator animator;

    public Transform transform_camera;
    public Vector2 v2_inputmove = Vector2.zero;
    public bool b_attackbuttonpressed = false;
    public Vector2 v2_attackbuttondrag;
    public bool b_attackbuttondraged = false;
    public bool b_rollbuttonpressed = false;
    public bool b_islocking = false;
    public bool b_isdefencing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playerstatus = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerstatus.programvalues.b_inputenable)
        {
            v2_inputmove = s_touchjoystick.GetInputVector();
            b_attackbuttonpressed = s_attackbutton.GetisPressed();
            v2_attackbuttondrag = s_attackbutton.GetDragDirection();
            b_attackbuttondraged = s_attackbutton.GetDraged();
            b_rollbuttonpressed = s_rollbutton.GetisPressed();
            b_islocking = s_lockbutton.GetisLocking();
            b_isdefencing = s_defencebutton.GetisPressed();
            touchcontroller.SetActive(true);
        }
        else
        {
            s_touchjoystick.ResetJoystick();
            v2_inputmove = Vector2.zero;
            b_attackbuttonpressed = false;
            b_attackbuttondraged = false;
            b_rollbuttonpressed = false;
            b_islocking = false;
            b_isdefencing = false;
            touchcontroller.SetActive(false);
        }
    }

    public void ResetAttackDrag()
    {
        s_attackbutton.ResetDraged();
    }

    public void ResetRollPressed()
    {
        s_rollbutton.Reset();
    }
}