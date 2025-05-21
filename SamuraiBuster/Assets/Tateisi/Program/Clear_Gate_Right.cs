using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Gate_Right : MonoBehaviour
{
    private bool GameClear;
    private void Start()
    {
        GameClear = false;
    }
    void Update()
    {
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        if (Input.GetKeyDown(KeyCode.C))
        {
            GameClear = true;
            Debug.Log("ゲームクリア(Cキー)が押されました。");
        }
        // クリアー時の処理
        if (GameClear)
        {
            if (vector.y <= 340.0f)
            {
                transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));
            }
        }
    }
}
