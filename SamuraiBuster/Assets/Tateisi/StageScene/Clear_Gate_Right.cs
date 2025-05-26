using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Gate_Right : MonoBehaviour
{

    void Update()
    {
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        // ƒNƒŠƒA[‚Ìˆ—
        if (GameDirector.Instance.IsGameCleared)
        {
            if (vector.y <= 340.0f)
            {
                transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));
            }
        }
    }
}
