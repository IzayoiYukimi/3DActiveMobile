using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator animator;

    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();   
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(animator.rootPosition);
        rb.MoveRotation(animator.rootRotation);
    }
}
