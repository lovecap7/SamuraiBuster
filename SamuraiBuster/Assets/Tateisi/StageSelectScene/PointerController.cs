using UnityEngine;
using UnityEngine.InputSystem;
using StageKind;

public class PointerController : MonoBehaviour
{

    // �V���O���g���C���X�^���X
    public static PointerController Instance { get; private set; }

    private Vector3 targetPos;

    public GameObject[] stageUIs = new GameObject[3];

    // �Q�[�����
    public bool IsSelect_1 { get; private set; }
    public bool IsSelect_2 { get; private set; }
    public bool IsSelect_3 { get; private set; }

    private StageKindEnum stageKind;

    private LightColor m_lightColor;


    private void Awake()
    {
        stageKind = StageKindEnum.Stage1; // �����X�e�[�W�ԍ���ݒ�

        // �V���O���g���C���X�^���X�̐ݒ�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        IsSelect_1 = false;
        IsSelect_2 = false;
        IsSelect_3 = false;
        targetPos = transform.position;

        m_lightColor = GameObject.Find("Directional Light").GetComponent<LightColor>();
    }
    void Update()
    {
        IsSelect_1 = false;
        IsSelect_2 = false;
        IsSelect_3 = false;
        if (stageKind == StageKindEnum.Stage1)
        {
            IsSelect_1 = true;
        }

        if (stageKind == StageKindEnum.Stage2)
        {
            IsSelect_2 = true;
        }

        if (stageKind == StageKindEnum.Stage3)
        {
            IsSelect_3 = true;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
    }


    public void LeftStageNum(InputAction.CallbackContext context)
    {
        // ���łɃX�e�[�W��I�����Ă���ꍇ�A�����Ȃ�
        if (selectstage_1.Instance.Stage1) return;
        if (selectstage_2.Instance.Stage2) return;
        if (selectstage_3.Instance.Stage3) return;
        if (context.started)
        {
            Debug.Log("LeftStageNum");
            SelectStateBack();   // �ЂƂ�̑I����Ԃɐi��  
        }
    }
    public void RightStageNum(InputAction.CallbackContext context)
    {
        // ���łɃX�e�[�W��I�����Ă���ꍇ�A�����Ȃ�
        if (selectstage_1.Instance.Stage1) return;
        if (selectstage_2.Instance.Stage2) return;
        if (selectstage_3.Instance.Stage3) return;
        if (context.started)
        {
            Debug.Log("RightStageNum");
            SelectStateProceed();      // �ЂƂO�̑I����Ԃɖ߂�
        }
    }

    /// <summary>
    /// �ЂƂ�̑I����Ԃɐi�ފ֐�
    /// </summary>
    private void SelectStateProceed()
    {
        stageKind = (StageKindEnum)(((int)stageKind + 1 + (int)StageKindEnum.StageNum) % (int)StageKindEnum.StageNum);
        targetPos.x = stageUIs[(int)stageKind].transform.position.x;
        m_lightColor.ChangeLightColor(stageKind);
    }

    /// <summary>
    /// �ЂƂO�̑I����Ԃɖ߂�֐�
    /// </summary>
    private void SelectStateBack()
    {
        stageKind = (StageKindEnum)(((int)stageKind - 1 + (int)StageKindEnum.StageNum) % (int)StageKindEnum.StageNum);
        targetPos.x = stageUIs[(int)stageKind].transform.position.x;
        m_lightColor.ChangeLightColor(stageKind);
    }
}
