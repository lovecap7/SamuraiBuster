using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public enum StateType//���
{
    Idle,   //�ҋ@
    Run,    //����
    Chase,  //�ǂ�������
    Back,   //������
    Attack, //�U��
    Hit,    //����
    Dead,   //���S
    Freeze, //�d��
}

abstract public class EnemyBase : MonoBehaviour
{
    //�v���C���[���܂Ƃ߂��I�u�W�F�N�g
    protected GameObject m_players;
    //�^�[�Q�b�g���
    protected GameObject[] m_targetList;
    //�^�[�Q�b�g
    protected GameObject m_target;

    //�^�[�Q�b�g�Ƃ̋���
    protected float m_targetDis = 0.0f;
    //�^�[�Q�b�g�ւ̃x�N�g��
    protected Vector3 m_targetDir = new Vector3();
    //�����̏��
    protected StateType m_nowState;
    protected StateType m_nextState;
    //���W�b�h�{�f�B
    protected Rigidbody m_rb;
    //�T�[�`�ɐ���������
    protected bool m_isHitSearch = false;

    //���̍U���܂łɂ����鎞��
    protected const float kAttackCoolTime = 3.0f;
    protected float m_attackCoolTime;

    //�A�j���[�V����
    protected Animator m_animator;
    protected bool m_isFinishAttackAnim = false;//�U���A�j���[�V�������I��������true
    protected bool m_isFinishHitAnim = false;//�q�b�g�A�j���[�V�������I��������true

    //��]���x
    protected const float kRotateSpeed = 10.0f;

    //���S�t���O
    [SerializeField] protected bool m_isDead = false;
    //���S�A�j���[�V�������I�������true
    protected bool m_isFinishDeadAnim = false;//���S�A�j���[�V�������I��������true

    //�̗͂ƃ_���[�W�̏����Ɏg���N���X
    protected CharacterStatus m_characterStatus;

    //fade���͓����Ȃ��łق���
    protected TransitionFade m_transitionFade;

    //�|�������̃X�R�A
    private GameObject m_score;
    protected int m_scorePoint = 0;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        m_rb = GetComponent<Rigidbody>();

        m_animator = GetComponent<Animator>();

        m_characterStatus = GetComponent<CharacterStatus>();
    
        m_attackCoolTime = kAttackCoolTime;

        //�v���C���[���܂Ƃ߂��I�u�W�F�N�g��T��
        m_players = GameObject.Find("Players");
        // �q�I�u�W�F�N�g�B������z��̏�����
        m_targetList = new GameObject[m_players.transform.childCount];
        for (int i = 0;i < m_targetList.Length;++i)
        {
            m_targetList[i] = m_players.transform.GetChild(i).gameObject;
        }

        //��ԋ߂��G���^�[�Q�b�g��
        SerchTarget();

        m_transitionFade = GameObject.Find("TransitionFade").GetComponent<TransitionFade>();
        m_score = GameObject.Find("Canvas/ScoreObject/Score");
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        //��ԋ߂��G���^�[�Q�b�g��
        SerchTarget();
        //�U���N�[���^�C��
        AttackCoolTime();
        //���S���
        CheckDead();
        //�t�F�[�h���͓����Ȃ�
        if(m_transitionFade.IsFadeNow())
        {
            ChangeState(StateType.Idle);
        }
    }
    virtual protected void ModelDir()//���f���̌���
    {
        if (!m_isHitSearch) return;//�^�[�Q�b�g�����Ȃ��Ȃ瑁�����^�[��
        //�����̌����擾
        Quaternion myDir = transform.rotation;
        Quaternion target = Quaternion.LookRotation(m_targetDir);
        //���񂾂񑊎�̂ق�������
        transform.rotation = Quaternion.RotateTowards(myDir, target, kRotateSpeed * Time.deltaTime);
    }

    //�A�j���[�V�����̍Đ���Ԃɍ��킹�ČĂяo��
    virtual public void OnFinishAnimAttack()
    {
        m_isFinishAttackAnim = true;
        Debug.Log("�U��");
    }
    virtual public void OffFinishAnimAttack()
    {
        m_isFinishAttackAnim = false;
    }

    virtual public void OnFinishAnimHit()
    {
        m_isFinishHitAnim = true;
    }
    virtual public void OnDead()
    {
        //�X�R�A�����Z����
       m_score.GetComponent<Score>().AddScore(m_scorePoint);
       Destroy(this.gameObject);
    }
    virtual public float GetHp()
    {
        return m_characterStatus.hitPoint;
    }

    virtual protected void CheckDead()
    {
        //�̗͂�0�ȉ��Ȃ玀�S
        if (m_characterStatus.hitPoint <= 0)
        {
            m_isDead = true;
        }
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    m_isDead = true;
        //}
    }
    abstract protected void AttackCoolTime();//�U���N�[���^�C��
    abstract protected void SerchTarget();//�����ƃ^�[�Q�b�g�̃x�N�g�����v�Z
    abstract protected void ChangeState(StateType state);//�����ƃ^�[�Q�b�g�̃x�N�g�����v�Z
    abstract protected void UpdateState();//�����ƃ^�[�Q�b�g�̃x�N�g�����v�Z
}
