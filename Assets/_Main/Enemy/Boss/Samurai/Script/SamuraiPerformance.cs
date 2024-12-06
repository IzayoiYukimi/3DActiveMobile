using UnityEngine;

public class SamuraiPerformance : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Enable",true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
