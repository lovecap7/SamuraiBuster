using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTime : MonoBehaviour
{
    [SerializeField] private float LigetRotaSpeed = 1.0f; // ���̉�]�ړ����x

    GameObject GameObject;

    private void FixedUpdate()
    {
        // ���̉�]�ړ�
        GameObject = this.gameObject;
        GameObject.transform.Rotate(Vector3.right, LigetRotaSpeed * Time.deltaTime, Space.World);
    }
}
