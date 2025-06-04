using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class RoleSelectController : MonoBehaviour
{
    // この順番でヒエラルキーに置いてある前提
    public enum RoleKind:int
    {
        Fighter,
        Healer,
        Mage,
        Tank,
        Max
    }

    // 選択されたロール
    public RoleKind selectedRole {  get; private set; }

    // プレイヤーがまとまっているオブジェクト
    [SerializeField]
    GameObject m_players; 

    private bool isDecided = false; // 決定済みフラグ
    public bool IsDecided()
    {
        return isDecided;
    }

    // 初期化処理
    void Start()
    {
        selectedRole = RoleKind.Fighter; // 初期ロールを設定
        m_players.transform.GetChild((int)selectedRole).gameObject.SetActive(true);
        transform.GetChild((int)selectedRole).gameObject.SetActive(true);
    }

    // 更新処理
    private void Update()
    {
    }

    /// <summary>
    /// ロール選択の上入力の処理
    /// </summary>
    /// <param name="context"></param>
    public void UpRole(InputAction.CallbackContext context)
    {
        if (isDecided) return; // 決定済みの場合は何もしない
        if (context.started)
        {
            Debug.Log("1P_UpRole");
            Back();
        }
    }

    /// <summary>
    /// ロール選択の下入力の処理
    /// </summary>
    /// <param name="context"></param>
    public void DownRole(InputAction.CallbackContext context)
    {
        if (isDecided) return; // 決定済みの場合は何もしない
        if (context.started)
        {
            Debug.Log("1P_DownRole");
            Proceed();
        }
    }


    public void Decide(InputAction.CallbackContext context)
    {
        Debug.Log("1P_True");
        isDecided = true; // 決定済みフラグを立てる
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        Debug.Log("1P_False");
        isDecided = false; // 決定をキャンセルする
    }

    /// <summary>
    /// ロール選択の上入力時の動き
    /// </summary>
    private void Proceed()
    {
        RoleKind before = selectedRole;
        selectedRole = (RoleKind)(((int)selectedRole + 1 + (int)RoleKind.Max) % (int)RoleKind.Max);
        transform.DOPunchPosition(new Vector3(0, 2, 0), 1.0f);
        ChangeIcon(before, selectedRole);
        ChangeModel(before, selectedRole);
    }

    /// <summary>
    /// ロール選択の下入力時の動き
    /// </summary>
    private void Back()
    {
        RoleKind before = selectedRole;
        selectedRole = (RoleKind)(((int)selectedRole - 1 + (int)RoleKind.Max) % (int)RoleKind.Max);
        transform.DOPunchPosition(new Vector3(0,2,0), 1.0f);
        ChangeIcon(before, selectedRole);
        ChangeModel(before, selectedRole);
    }

    private void ChangeIcon(RoleKind beforeRole, RoleKind nextRole)
    {
        // 今までのアイコンを消す
        transform.GetChild((int)beforeRole).gameObject.SetActive(false);
        // 次をつける
        transform.GetChild((int)nextRole).gameObject.SetActive(true);
    }

    private void ChangeModel(RoleKind beforeRole, RoleKind nextRole)
    {
        m_players.transform.GetChild((int)beforeRole).gameObject.SetActive(false);
        m_players.transform.GetChild((int)nextRole).gameObject.SetActive(true);
    }
}
