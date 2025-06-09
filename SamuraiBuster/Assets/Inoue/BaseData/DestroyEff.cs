using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEff : MonoBehaviour
{
    private const float kDestroyEffActiveTime = 1.0f;
    void Update()
    {
        Destroy(this.gameObject, kDestroyEffActiveTime);
    }
}
