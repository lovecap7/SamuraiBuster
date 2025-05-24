using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Gate_Left : MonoBehaviour
{
    void Update()
    {
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        // ƒNƒŠƒA[Žž‚Ìˆ—
        if (GameDirector.Instance.IsGameCleared)
        {
            if (vector.y >= 200.0f)
            {
                transform.Rotate(new Vector3(0.0f, -0.5f, 0.0f));
            }
        }
    }
}
