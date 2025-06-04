using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSoundScriipt : MonoBehaviour
{
    // 自分が既に存在しているかを確認するためのフラグ
    private static bool isInstanceExisnt = false;
    private void Awake()
    {
        if (isInstanceExisnt)
        { 
            // すでにロードされていたら
            Destroy(this.gameObject); // 自分自身を破棄して終了
            return;
        }
        // 存在することを記録
        isInstanceExisnt = true;
        // シーンを遷移してもオブジェクトを破棄しないようにする
        DontDestroyOnLoad(this.gameObject);
    }

}
//using UnityEngine.SceneManagement;

//public class MainSoundScriipt : MonoBehaviour
//{
//    // シーン遷移時にBGMを再生するためのスクリプト
//    // このスクリプトは、
//    // タイトルシーン,ステージセレクトシーン,ロールセレクトシーン
//    // にBGMを流すために使用されます。

//    // シングルトンの設定

//    static public MainSoundScriipt insnancs;

//    private void Awake()
//    {
//        if (insnancs == null)
//        {
//            // シングルトンのインスタンスが存在しない場合
//            insnancs = this; // インスタンスを設定
//            DontDestroyOnLoad(this.gameObject); // シーン遷移してもオブジェクトを破棄しないようにする
//        }
//        else
//        {
//            // すでにシングルトンのインスタンスが存在する場合
//            Destroy(this.gameObject); // 自分自身を破棄して終了
//        }
//    }
//    // シングルトンの設定終わり





//    public AudioSource bgm; // AudioSource型の変数bgmを宣言　対応するAudioSourceコンポーネントをアタッチ

//    public string sceneName; // シーン名を格納する変数

//    private void Start()
//    {
//        sceneName = "TitleScene"; // 初期シーン名を設定
//        bgm.Play(); // BGMを再生

//        // シーンが変更されたときに呼び出されるイベントを登録
//        SceneManager.activeSceneChanged += OnActiveSceneChanged;
//    }

//    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
//    {
//        // シーンがどう変わったかを確認

//        // TitleSceneからStageSelectSceneへ
//        if (sceneName == "TitleScene" && nextScene.name == "SceneSelectScene")
//        {
//            sceneName = "SceneSelectScene"; // シーン名を更新
//            bgm.Play(); // BGMを再生
//        }

//        // StageSelectSceneからRollSelectSceneへ
//        if (sceneName == "SceneSelectScene" && nextScene.name == "1PlayRollSelectScene")
//        {
//            sceneName = "1PlayRollSelectScene"; // シーン名を更新
//            bgm.Play(); // BGMを再生
//        }

//        // RoleSelectSceneからStageSceneへ
//        if (sceneName == "1PlayRollSelectScene" && nextScene.name == "StageScene")
//        {
//            sceneName = "StageScene"; // シーン名を更新
//            bgm.Stop(); // BGMを停止
//        }

//        //遷移後のシーン名を「１つ前のシーン名」として保持
//        sceneName = nextScene.name;

//    }
//}