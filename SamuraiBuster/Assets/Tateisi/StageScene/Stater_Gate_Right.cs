using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stater_Gate_Right : MonoBehaviour
{

    void Update()
    {
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        // スタート時の処理
        if (GameDirector.Instance.IsGameStarted)
        {
            if (vector.y <= 170.0f)
            {
                transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));
            }
        }
    }
}
