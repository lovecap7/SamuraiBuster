using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

enum  SelectState
{
    StageSelect,
    PlayerNumSelect,
    RoleSelect,
    TitleBack
}
public class SelectDirector : MonoBehaviour
{
    [SerializeField] private SelectState selectState;
    [SerializeField] GameObject stageSelectText;
    [SerializeField] CanvasGroup stageCursorImages;
    //このシーンでは二つのオブジェクトがフェードを持ってるので
    //フェードアウトをした本人が誰かを把握しなければいけない
    [SerializeField] FadeManager m_fadeManager;
    bool m_isFadeOwner = false;
    private void Awake()
    {
        selectState = SelectState.StageSelect;
    }

    private void Start()
    {
        //フェードインする
        m_fadeManager.m_isFadeIn = true;
    }

    private void Update()
    {
        if(m_fadeManager.m_fadeAlpha >= 1.0f && m_isFadeOwner)
        {
            TryChangeScene();
        }
    }

    // 決定か戻るボタンが押されるたびに呼ばれる関数を作る
    // その関数が呼ばれたらselectStateを進めるか戻す関数を呼ぶ
    // 進める関数は
    // TitleBackならStageSelect,StageSelectならPlayerNumSelect...というように進める
    // 戻る関数はその逆

    public void SelectStateOK(InputAction.CallbackContext context)
    {
        if(!context.performed)return;
        Debug.Log("OK");
        SelectStateProceed();   // ひとつ先の選択状態に進む
    }

    public void SelectStateBack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log("Back");
        SelectStateBack();      // ひとつ前の選択状態に戻る 
    }

    public void TryChangeScene()
    {
        if(!(selectState == SelectState.RoleSelect || 
             selectState == SelectState.TitleBack))
        {
            Debug.Log("Nooooo!!!!");
            return;
        }
        if(selectState == SelectState.TitleBack)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
        }
        if (selectState == SelectState.RoleSelect)
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene("RollSelectScene");
        }
    }

    /// <summary>
    /// ひとつ先の選択状態に進む関数
    /// </summary>
    private void SelectStateProceed()
    {
        if(selectState == SelectState.StageSelect)
        {
            selectState = SelectState.PlayerNumSelect;
            // ステージ選択のテキストを消す
            stageSelectText.SetActive(false);
            // ステージ選択のカーソルを消す
            stageCursorImages.alpha = 0;
            return;
        }
        if(selectState == SelectState.PlayerNumSelect)
        {
            selectState = SelectState.RoleSelect;
            return;
        }
    }
    /// <summary>
    /// ひとつ前の選択状態に戻る関数
    /// </summary>
    private void SelectStateBack()
    {
        // 人数選択の入力できない期間を復活
        IsNumselect.Instance.cantSelectFrame = IsNumselect.kCantSelectFrameCount;

        if (selectState == SelectState.PlayerNumSelect)
        {
            selectState = SelectState.StageSelect;
            // ステージ選択のテキストを出す
            stageSelectText.SetActive(true);
            // ステージ選択のカーソルを出す
            stageCursorImages.alpha = 1;
            return;
        }
        if (selectState == SelectState.StageSelect)
        {
            selectState = SelectState.TitleBack;
            return;
        }
    }

    /// <summary>
    /// 右に移動するための入力処理
    /// </summary>
    /// <param name="context"></param>
    public void TitleBack(InputAction.CallbackContext context)
    {
        if (m_fadeManager.m_isFadeOut) return;
        if (selectstage_1.Instance.Stage1) return;// 選択後は入力を受け付けない
        if (selectstage_2.Instance.Stage2) return;// 選択後は入力を受け付けない
        if (selectstage_3.Instance.Stage3) return;// 選択後は入力を受け付けない
        //ボタンを押したとき
        if (context.performed)
        {
           m_fadeManager.m_isFadeOut = true;
           m_isFadeOwner = true;
        }
    }
}
