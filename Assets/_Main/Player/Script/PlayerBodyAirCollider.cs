using UnityEngine;

public class PlayerBodyAirCollider : MonoBehaviour
{
    [SerializeField] private Transform transform_rootm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform_rootm.position;
    }
}
