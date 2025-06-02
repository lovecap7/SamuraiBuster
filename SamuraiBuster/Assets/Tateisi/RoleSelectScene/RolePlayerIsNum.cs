using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePlayerIsNum : MonoBehaviour
{
    [SerializeField] private bool IsPlayerNum_1;  // プレイヤー1が選択されているかどうか
    [SerializeField] private bool IsPlayerNum_2;  // プレイヤー2が選択されているかどうか
    [SerializeField] private bool IsPlayerNum_3;  // プレイヤー3が選択されているかどうか
    [SerializeField] private bool IsPlayerNum_4;  // プレイヤー4が選択されているかどうか
    private void Update()
    {
        // プレイヤー1が選択されているかどうかをチェック
        IsPlayerNum_1 = NumPointerController.Instance.IsPlayerNum_1;
        // プレイヤー2が選択されているかどうかをチェック
        IsPlayerNum_2 = NumPointerController.Instance.IsPlayerNum_2;
        // プレイヤー3が選択されているかどうかをチェック
        IsPlayerNum_3 = NumPointerController.Instance.IsPlayerNum_3;
        // プレイヤー4が選択されているかどうかをチェック
        IsPlayerNum_4 = NumPointerController.Instance.IsPlayerNum_4;
    }


}
