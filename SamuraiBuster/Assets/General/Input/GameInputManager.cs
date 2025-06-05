using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    // こちらからはどんなインスタンスがあるか分からないので、外から入れてもらう
    public List<IInputReceiver> m_receivers = new();
    List<GameInputHolder> m_inputHolders = new();

    // Start is called before the first frame update
    void Start()
    {
        // Input関連を消えないようにする
        // これがゲームを通して存在することで、デバイスがシャッフルされるのを防ぐ
        DontDestroyOnLoad(gameObject);

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
}
