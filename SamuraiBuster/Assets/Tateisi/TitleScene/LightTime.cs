using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTime : MonoBehaviour
{
    [SerializeField] private float LigetRotaSpeed = 1.0f; // 光の回転移動速度

    GameObject GameObject;

    private void FixedUpdate()
    {
        // 光の回転移動
        GameObject = this.gameObject;
        GameObject.transform.Rotate(Vector3.right, LigetRotaSpeed * Time.deltaTime, Space.World);
    }
}
