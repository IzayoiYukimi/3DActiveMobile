using UnityEngine;

public class SamuraiClone : MonoBehaviour
{
    float f_lifetime = 2.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GetComponent<Animator>().speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        f_lifetime-=Time.deltaTime;
        if (f_lifetime<=0)
        {
            gameObject.SetActive(false);
        }
    }
}
