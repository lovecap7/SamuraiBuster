using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearGateBox : MonoBehaviour
{
    // 移動のスムーズさ（大きいほど速い）
    [SerializeField] private float moveSpeed;
    // どこまで移動するか座標
    [SerializeField] private float moveOffsetY;

    private Vector3 targetPosition;     // 目標位置
    private bool isMoving = false;      // 移動中フラグ
    private bool hasMoveed = false; // 1度だけワープするためのフラグ
    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        HandleInput();    // 入力受付
        MovePointer();    // ポインターの移動
    }
    private void HandleInput()
    {
        if (isMoving || hasMoveed) return;
        // クリアー時の処理
        if (GameDirector.Instance.IsGameCleared)
        {
            WarpTo(targetPosition.y + moveOffsetY);
            hasMoveed = true;
        }
    }



    private void MovePointer()
    {
        if (!isMoving) return;
        // Lerpでスムーズに移動
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        // 移動完了判定
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
