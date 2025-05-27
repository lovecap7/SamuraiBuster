using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerCommon;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    GameObject m_players;

    int m_playersCount;
    const int kMaxPlayerCount = 4;

    // Player(n)グループがアイコンを上から何番目に持っているか
    const int kIconsIndex = 2;

    void Start()
    {
        // 人数把握
        m_playersCount = m_players.transform.childCount;

        // プレイヤーが四人以上いるときも開発中ならあり得るので、バグ予防
        if (m_playersCount > kMaxPlayerCount) m_playersCount = kMaxPlayerCount;

        // その分UIを有効化
        for (int i = 0; i < m_playersCount; ++i)
        {
            var UI = transform.GetChild(i).gameObject;
            UI.SetActive(true);

            var player = m_players.transform.GetChild(i);

            // プレイヤーの役職によってイラストを変える
            var role = player.GetComponent<PlayerBase>().GetRole();

            // 役職に応じてUIのスプライトを設定
            UI.transform.GetChild(kIconsIndex).GetChild((int)role).gameObject.SetActive(true);

            // それぞれのUIにプレイヤーを割り当てる
            UI.GetComponent<SkillBar>().SetPlayer(ref player.gameObject);
        }
    }
}
