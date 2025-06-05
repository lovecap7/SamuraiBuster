using DG.Tweening;
using UnityEngine;

public class RoleSelectController : MonoBehaviour, IInputReceiver
{
    private Tweener m_punch;
    private Vector3 initPos;

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
        m_players.transform.GetChild((int)selectedRole).gameObject.SetActive(true);
        transform.GetChild((int)selectedRole).gameObject.SetActive(true);

        // �������ォ�牽�Ԗڂ��ŕR�Â���PlayerInput�����߂�
        GameInputManager gameInputManager = GameObject.Find("PlayerInputs").GetComponent<GameInputManager>();
        gameInputManager.m_receivers.Add(this);

        initPos = transform.localPosition;
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
        m_players.transform.GetChild((int)beforeRole).gameObject.SetActive(false);
        m_players.transform.GetChild((int)nextRole).gameObject.SetActive(true);
    }

    public void Submit()
    {
        isDecided = true; // ����ς݃t���O�𗧂Ă�
    }

    public void Cancel()
    {
        isDecided = false; // ������L�����Z������
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
