using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

//[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class MainSoundScriipt : MonoBehaviour
{
    // 自分が既に存在しているかを確認するためのフラグ  
    private static bool isInstanceExisnt = false;

    public void Awake()
    {
        // 既に存在している場合は、オブジェクトを破棄して終了  
        if (isInstanceExisnt)
        {
            Destroy(this.gameObject);
            return;
        }
        // 存在することを記録  
        isInstanceExisnt = true;
        // シーンを遷移してもオブジェクトを破棄しないようにする  
        DontDestroyOnLoad(this.gameObject);
    }


    private void Update()
    {
        if ((SceneManager.GetActiveScene().name == "Stage1Scene") ||
            (SceneManager.GetActiveScene().name == "Stage2Scene") ||
            (SceneManager.GetActiveScene().name == "Stage3Scene"))
        {
            Destroy(this.gameObject); // 自分自身を破棄して終了  
            return;
        }
        // シーンを遷移してもオブジェクトを破棄しないようにする  
        DontDestroyOnLoad(this.gameObject);
    } // 修正: Update メソッドの閉じ括弧を追加  

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
