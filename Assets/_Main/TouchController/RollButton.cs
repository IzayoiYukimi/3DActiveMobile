using System;
using UnityEngine;

public class RollButton : MonoBehaviour
{
    [SerializeField]private bool b_ispressed = false;
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
