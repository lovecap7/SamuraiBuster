using UnityEngine;
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
    private void Awake()
    {
        selectState = SelectState.StageSelect;
    }

    // ���肩�߂�{�^����������邽�тɌĂ΂��֐������
    // ���̊֐����Ă΂ꂽ��selectState��i�߂邩�߂��֐����Ă�
    // �i�߂�֐���
    // TitleBack�Ȃ�StageSelect,StageSelect�Ȃ�PlayerNumSelect...�Ƃ����悤�ɐi�߂�
    // �߂�֐��͂��̋t

    public void SelectStateOK(InputAction.CallbackContext context)
    {
        if(!context.performed)return;
        Debug.Log("OK");
        SelectStateProceed();   // �ЂƂ�̑I����Ԃɐi��
    }

    public void SelectStateBack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log("Back");
        SelectStateBack();      // �ЂƂO�̑I����Ԃɖ߂� 
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
    /// �ЂƂ�̑I����Ԃɐi�ފ֐�
    /// </summary>
    private void SelectStateProceed()
    {
        if(selectState == SelectState.StageSelect)
        {
            selectState = SelectState.PlayerNumSelect;
            return;
        }
        if(selectState == SelectState.PlayerNumSelect)
        {
            selectState = SelectState.RoleSelect;
            return;
        }
    }
    /// <summary>
    /// �ЂƂO�̑I����Ԃɖ߂�֐�
    /// </summary>
    private void SelectStateBack()
    {
        // �l���I���̓��͂ł��Ȃ����Ԃ𕜊�
        IsNumselect.Instance.cantSelectFrame = IsNumselect.kCantSelectFrameCount;

        if (selectState == SelectState.PlayerNumSelect)
        {
            selectState = SelectState.StageSelect;
            return;
        }
        if (selectState == SelectState.StageSelect)
        {
            selectState = SelectState.TitleBack;
            return;
        }
    }

    /// <summary>
    /// �E�Ɉړ����邽�߂̓��͏���
    /// </summary>
    /// <param name="context"></param>
    public void TitleBack(InputAction.CallbackContext context)
    {
        if (selectstage_1.Instance.Stage1) return;// �I����͓��͂��󂯕t���Ȃ�
        if (selectstage_2.Instance.Stage2) return;// �I����͓��͂��󂯕t���Ȃ�
        if (selectstage_3.Instance.Stage3) return;// �I����͓��͂��󂯕t���Ȃ�
        //�{�^�����������Ƃ�
        if (context.performed)
        {
            TryChangeScene();
        }
    }
}
