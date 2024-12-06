using UnityEngine;
using UnityEngine.UI;

public class LockButton : MonoBehaviour
{
    [SerializeField]private bool b_islocking = false;

    [SerializeField] private Image image_lockedicon = null;
    
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
        image_lockedicon.enabled = b_islocking;
    }

    public bool GetisLocking()
    {
        return b_islocking;
    }
   
}
