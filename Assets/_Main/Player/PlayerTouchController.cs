using UnityEngine;

public class PlayerTouchController : MonoBehaviour
{
    [SerializeField] private TouchJoystick s_touchjoystick;

    [SerializeField] private AttackButton s_attackbutton;
    
    [SerializeField]private RollButton s_rollbutton;
    
    [SerializeField]private Animator animator;

    public Transform transform_camera;
    public Vector2 v2_inputmove = Vector2.zero;
    public bool b_attackbuttonpressed = false;
    public Vector2 v2_attackbuttondrag;
    public bool b_attackbuttondraged = false;
    public bool b_rollbuttonpressed = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        v2_inputmove = s_touchjoystick.GetInputVector();
        
        b_attackbuttonpressed = s_attackbutton.GetisPressed();
        v2_attackbuttondrag = s_attackbutton.GetDragDirection();
        b_attackbuttondraged = s_attackbutton.GetDraged();
        
        b_rollbuttonpressed=s_rollbutton.GetisPressed();
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