using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RolePlayerIsNum : MonoBehaviour
{
    private List<RoleSelectController> roleSelects;

    private void Start()
    {
        // プレイヤーの人数を把握
        roleSelects = new List<RoleSelectController>();
        int playerNum = PlayerPrefs.GetInt("PlayerNum");
        for (int i = 0; i < playerNum; ++i)
        {
            roleSelects.Add(transform.GetChild(i).GetComponent<RoleSelectController>());
        }
    }

    private void Update()
    {
        bool allDicided = true;
        if (roleSelects.Count == 0)
        {
            //Debug.Log("プレイヤーが0人です");
            return;
        }
        // 全員が選択しているかチェック
        foreach (var role in roleSelects)
        {
            allDicided &= role.IsDecided();
        }

        if (!allDicided) return;

        int playerId = 0;

        // それぞれが今選んでいる役職を記憶
        foreach (var role in roleSelects)
        {
            string saveString = "PlayerRole" + playerId.ToString();
            PlayerPrefs.SetInt(saveString, (int)role.selectedRole);
            ++playerId;
        }

        // プレイヤーを生成
        GameObject.Find("Players").GetComponent<PlayerGenerator>().GeneratePlayer();

        // 行ってらっしゃい
        // 先のシーンで選んでいたシーンへ飛ぶ
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage" + PlayerPrefs.GetInt("StageNum").ToString() + "Scene");
    }
}
