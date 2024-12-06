using UnityEngine;

public class DefenceButton : MonoBehaviour
{
    [SerializeField]private bool b_ispressed = false;
    public void OnClick()
    {
        if (!b_ispressed)
        {
            b_ispressed = true;
        }
    }

    public bool GetisPressed()
    {
        return b_ispressed;
    }
    public void Reset()
    {
        b_ispressed = false;
    }
}
