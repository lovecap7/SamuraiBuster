using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearGateBox : MonoBehaviour
{
    // �ړ��̃X���[�Y���i�傫���قǑ����j
    [SerializeField] private float moveSpeed;
    // �ǂ��܂ňړ����邩���W
    [SerializeField] private float moveOffsetY;

    private Vector3 targetPosition;     // �ڕW�ʒu
    private bool isMoving = false;      // �ړ����t���O
    private bool hasMoveed = false; // 1�x�������[�v���邽�߂̃t���O
    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        HandleInput();    // ���͎�t
        MovePointer();    // �|�C���^�[�̈ړ�
    }
    private void HandleInput()
    {
        if (isMoving || hasMoveed) return;
        // �N���A�[���̏���
        if (GameDirector.Instance.IsGameCleared)
        {
            WarpTo(targetPosition.y + moveOffsetY);
            hasMoveed = true;
        }
    }



    private void MovePointer()
    {
        if (!isMoving) return;
        // Lerp�ŃX���[�Y�Ɉړ�
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        // �ړ���������
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
    private void WarpTo(float newY)
    {
        targetPosition = new Vector3(targetPosition.x, newY, targetPosition.z);
        isMoving = true;
    }
}
