using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stater_Gate_Right : MonoBehaviour
{
    private bool GameStater;
    private void Start()
    {
        GameStater = false;
    }
    void Update()
    {
        Transform transform = this.transform;
        Vector3 vector = transform.eulerAngles;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameStater = true;
            Debug.Log("ゲームクリア(Zキー)が押されました。");
        }
        // クリアー時の処理
        if (GameStater)
        {
            if (vector.y <= 170.0f)
            {
                transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));
            }
        }
    }
}
