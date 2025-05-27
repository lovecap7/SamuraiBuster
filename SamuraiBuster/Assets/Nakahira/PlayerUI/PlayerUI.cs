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

    // 子オブジェクトの順番
    // 変えたときは要確認
    enum UIIndex
    {
        HPBar,
        SkillBar,
        Icons
    };

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
            var playerBase = player.GetComponent<PlayerBase>();

            // プレイヤーの役職によってイラストを変える
            var role = playerBase.GetRole();

            // 役職に応じてUIのスプライトを設定
            UI.transform.GetChild((int)UIIndex.Icons).GetChild((int)role).gameObject.SetActive(true);

            // それぞれのUIにプレイヤーを割り当てる
            UI.transform.GetChild((int)UIIndex.HPBar).GetComponent<HPBar>().SetPlayer(ref playerBase);
            UI.transform.GetChild((int)UIIndex.SkillBar).GetComponent<SkillBar>().SetPlayer(ref playerBase);
        }
    }
}
