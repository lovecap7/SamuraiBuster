using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrameRate : MonoBehaviour
{
    public void Start()
    {
        Application.targetFrameRate = 60;
    }
}
