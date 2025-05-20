using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Player : MonoBehaviour
{
    float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.position += speed * transform.forward * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.position -= speed * transform.forward * Time.deltaTime;
        }
    }
}
