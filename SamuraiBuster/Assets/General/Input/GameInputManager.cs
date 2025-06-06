using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameInputManager : MonoBehaviour
{
    [SerializeField] private GameObject m_inputPrefab;

    // こちらからはどんなインスタンスがあるか分からないので、外から入れてもらう
    List<IInputReceiver> m_receivers = new();
    List<GameInputHolder> m_inputHolders = new();

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // 1フレ遅らせる
        yield return null;

        // シーンが切り替わったときに実行する関数を登録
        SceneManager.sceneLoaded += SceneLoded;

        // Input関連を消えないようにする
        // これがゲームを通して存在することで、デバイスがシャッフルされるのを防ぐ
        DontDestroyOnLoad(gameObject);

        // プレイヤーの人数分入力機構を生成
        int playerNum = PlayerPrefs.GetInt("PlayerNum");
        var pad = Gamepad.all;
        int padCount = pad.Count;
        for (int i = 0; i < playerNum; ++i)
        {
            Gamepad gamepad;

            // 例えば四人プレイを選択して3台しかコントローラがつながっていなければ
            if (i > padCount - 1) // ※要素数とインデックスをそろえている
            {
                // その分のコントローラ割り当ては保留
                // 生成はしたいので、nullを入れとく
                gamepad = null;
            }
            else
            {
                gamepad = pad[i];
            }

            var instance = PlayerInput.Instantiate(m_inputPrefab, i, "Game", -1, gamepad);
            instance.transform.SetParent(transform, false);
        }

        // 数だけ取得
        for (int i = 0; i < transform.childCount; ++i)
        {
            m_inputHolders.Add(transform.GetChild(i).GetComponent<GameInputHolder>());
        }

        SetInterface();
    }

    // 各GameInputに対応したインターフェースを渡す
    void SetInterface()
    {
        // 同じだけ存在すると思いたい
        int id = 0;
        foreach (var holder  in m_inputHolders)
        {
            holder.receiver = m_receivers[id];
            ++id;
        }
    }

    public void AddReceiver(IInputReceiver receiver)
    {
        m_receivers.Add(receiver);
    }

    private void SceneLoded(Scene nextScene, LoadSceneMode mode)
    {
        // シーンが切り替わったら、前のシーンにあったReceiverは消えるので削除しておく
        m_receivers.Clear();

        foreach (var holder in m_inputHolders)
        {
            holder.receiver = null;
        }

        StartCoroutine(SetInterfaceCoroutine());
    }

    IEnumerator SetInterfaceCoroutine()
    {
        yield return null;

        SetInterface();
    }
}
