using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTime : MonoBehaviour
{
    [SerializeField] private float LigetRotaSpeed = 1.0f; // Œõ‚Ì‰ñ“]ˆÚ“®‘¬“x

    GameObject GameObject;

    private void FixedUpdate()
    {
        // Œõ‚Ì‰ñ“]ˆÚ“®
        GameObject = this.gameObject;
        GameObject.transform.Rotate(Vector3.right, LigetRotaSpeed * Time.deltaTime, Space.World);
    }
}
