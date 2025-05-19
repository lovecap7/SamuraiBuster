using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = GameObject.Find("canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.transform.position += new Vector3(-300.0f, 0.0f, 0.0f);//Lerp‚Å‚â‚è‚½‚¢
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(300.0f, 0.0f, 0.0f);//Lerp‚Å‚â‚è‚½‚¢
        }
    }
}
