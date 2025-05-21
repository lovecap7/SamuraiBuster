using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    private bool GameStart;
    private bool GameClear;
    private void Start()
    {
        GameStart = false;
        GameClear = false;
    }
    void Update()
    {
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;
        if (GameStart)
        {
            transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));
        }
        if (!GameClear)
        {
            if(vector.y >= -170.0f)
            {
                transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));
                Debug.Log("•Ï”‚Ì’l:" + vector.y);
            }
        }
    }
}
