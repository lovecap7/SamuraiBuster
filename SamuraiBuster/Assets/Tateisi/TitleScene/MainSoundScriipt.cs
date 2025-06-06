using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class MainSoundScriipt : MonoBehaviour
{
    // 自分が既に存在しているかを確認するためのフラグ  
    private static bool isInstanceExisnt = false;

    private void Update()
    {
        if (isInstanceExisnt)
        {
            if (SceneManager.GetActiveScene().name == "StageScene")
            {
                Destroy(this.gameObject); // 自分自身を破棄して終了  
                return;
            }
        }
        // 存在することを記録  
        isInstanceExisnt = true;
        // シーンを遷移してもオブジェクトを破棄しないようにする  
        DontDestroyOnLoad(this.gameObject);
    } // 修正: Update メソッドの閉じ括弧を追加  

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
