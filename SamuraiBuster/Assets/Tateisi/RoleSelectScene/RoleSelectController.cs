using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoleSelectController : MonoBehaviour, IInputReceiver
{
    private Tweener m_punch;
    private Vector3 initPos;
    private GameObject m_upArrow;
    private GameObject m_downArrow;
    private GameObject m_activeStandModel;

    // ���̏��ԂŃq�G�����L�[�ɒu���Ă���O��
    public enum RoleKind:int
    {
        Fighter,
        Healer,
        Mage,
        Tank,
        Max
    }

    // �I�����ꂽ���[��
    public RoleKind selectedRole {  get; private set; }

    // �v���C���[���܂Ƃ܂��Ă���I�u�W�F�N�g
    [SerializeField]
    GameObject m_players; 

    private bool isDecided = false; // ����ς݃t���O
    public bool IsDecided()
    {
        return isDecided;
    }

    // ����������
    void Start()
    {
        selectedRole = RoleKind.Fighter; // �������[����ݒ�
        m_activeStandModel = m_players.transform.GetChild((int)selectedRole).gameObject;
        m_activeStandModel.SetActive(true);
        transform.GetChild((int)selectedRole).gameObject.SetActive(true);

        // �������ォ�牽�Ԗڂ��ŕR�Â���PlayerInput�����߂�
        GameInputManager gameInputManager = GameObject.Find("PlayerInputs").GetComponent<GameInputManager>();
        gameInputManager.AddReceiver(this);

        initPos = transform.localPosition;

        m_upArrow = transform.GetChild(4).gameObject;
        m_downArrow = transform.GetChild(5).gameObject;
    }

    // �X�V����
    private void Update()
    {
    }

    /// <summary>
    /// ���[���I���̏���͎��̓���
    /// </summary>
    private void Up()
    {
        m_punch?.Kill();
        transform.localPosition = initPos;

        RoleKind before = selectedRole;
        selectedRole = (RoleKind)(((int)selectedRole - 1 + (int)RoleKind.Max) % (int)RoleKind.Max);
        m_punch = transform.DOPunchPosition(new Vector3(0,10,0), 0.2f);
        ChangeIcon(before, selectedRole);
        ChangeModel(before, selectedRole);
    }

    /// <summary>
    /// ���[���I���̉����͎��̓���
    /// </summary>
    private void Down()
    {
        m_punch?.Kill();
        transform.localPosition = initPos;

        RoleKind before = selectedRole;
        selectedRole = (RoleKind)(((int)selectedRole + 1 + (int)RoleKind.Max) % (int)RoleKind.Max);
        m_punch = transform.DOPunchPosition(new Vector3(0,-10,0), 0.2f);
        ChangeIcon(before, selectedRole);
        ChangeModel(before, selectedRole);
    }

    private void ChangeIcon(RoleKind beforeRole, RoleKind nextRole)
    {
        // ���܂ł̃A�C�R��������
        transform.GetChild((int)beforeRole).gameObject.SetActive(false);
        // ��������
        transform.GetChild((int)nextRole).gameObject.SetActive(true);
    }

    private void ChangeModel(RoleKind beforeRole, RoleKind nextRole)
    {
        m_activeStandModel.SetActive(false);
        m_activeStandModel = m_players.transform.GetChild((int)nextRole).gameObject;
        m_activeStandModel.SetActive(true);
    }

    public void Submit()
    {
        isDecided = true; // ����ς݃t���O�𗧂Ă�

        transform.DOPunchScale(Vector3.one, 0.2f);
        m_upArrow.GetComponent<Image>().color = Color.gray;
        m_downArrow.GetComponent<Image>().color = Color.gray;
        // �v���C���[�ɃG���[�g������
        m_activeStandModel.GetComponent<Animator>().SetTrigger("Emote");
    }

    public void Cancel()
    {
        isDecided = false; // ������L�����Z������
        m_upArrow.GetComponent<Image>().color = Color.white;
        m_downArrow.GetComponent<Image>().color = Color.white;
        // �v���C���[�ɃG���[�g������
        // �����g���K�[�ŃL�����Z���������Ă܂�
        m_activeStandModel.GetComponent<Animator>().SetTrigger("Emote");
    }

    public void TriggerUp()
    {
        if (isDecided) return; // ����ς݂̏ꍇ�͉������Ȃ�
        Up();
    }

    public void TriggerDown()
    {
        if (isDecided) return; // ����ς݂̏ꍇ�͉������Ȃ�

        Down();
    }

    public void TriggerRight()
    {
        return;
    }

    public void TriggerLeft()
    {
        return;
    }

    public void Attack()
    {
        return;
    }

    public void Skill()
    {
        return;
    }

    public void Move(Vector2 axis)
    {
        // �������Ȃ�
        return;
    }
}
