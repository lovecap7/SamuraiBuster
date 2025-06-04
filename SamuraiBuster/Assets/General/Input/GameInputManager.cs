using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class GameInputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Input関連を消えないようにする
        // これがゲームを通して存在することで、デバイスがシャッフルされるのを防ぐ
        DontDestroyOnLoad(gameObject);
    }
}
