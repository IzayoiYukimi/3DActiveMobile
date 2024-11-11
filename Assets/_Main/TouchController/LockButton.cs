using UnityEngine;

public class LockButton : MonoBehaviour
{
    [SerializeField]private bool b_islocking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        b_islocking = !b_islocking;
    }

    public bool GetisLocking()
    {
        return b_islocking;
    }
   
}
