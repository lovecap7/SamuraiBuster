using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : EnemyBase
{
    //���肩�疂�@���o��
    [SerializeField] private GameObject m_leftHand;
    //�̗�
    private const int kHP = 900;
    //�_���[�W
    private const int kDamage = 100;
    // Start is called before the first frame update
    //�e
    [SerializeField] private GameObject m_magicShotPrefab;
    //�e�̑��x
    private const float kShotSpeed = 5.0f;
    //�߂Â����x
    private const float kChaseSpeed = 30.0f;
    //�G�����ꂷ���Ă���Ƌ߂Â�
    private const float kChaseDis = 3.0f;
    //����鑬�x
    private const float kBackSpeed = 0.1f;
    //�߂�����Ɨ����
    private const float kBackDis = 1.2f;
    //�o�b�N�X�e�b�v�̃N�[���^�C��
    private const float kBackCoolTime = 5.0f;
    private float m_backCoolTime = 0;
    //�o�b�N�X�e�b�v�̃A�j���[�V�������I��������ǂ���
    private bool m_isFinishAnimBack = false;
    //�̂�����
    private const float kKnockBackForce = 1.1f;
    //�X�R�A
    private const int kScorePoint = 1500;
    override protected void Start()
    {
        base.Start();
        //�̗�
        //�l���������ꍇ�����̗͂�������
        int addHp = 0;
        if (m_targetList.Length > 2) addHp = 200;
        m_characterStatus.hitPoint = kHP + addHp;
        //�̗̓o�[�ɐݒ�
        Slider hpBar = transform.Find("Canvas_Hp/Hpbar").gameObject.GetComponent<Slider>();
        hpBar.maxValue = m_characterStatus.hitPoint;

        //�ҋ@���
        m_nowState = StateType.Idle;
        m_nextState = m_nowState;
        //�X�R�A
        m_scorePoint = kScorePoint;
    }

    public void OnFinishAnimBack()//�A�j���[�V�������Ăяo��
    {
        m_isFinishAnimBack = true;
    }
    public void OffFinishAnimBack()//�A�j���[�V�������Ăяo��
    {
        m_isFinishAnimBack = false;
    }
    private void BackForce()//�A�j���[�V�������Ăяo��
    {
        //�o�b�N�X�e�b�v
        if (m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();
        }
        m_rb.AddForce(m_targetDir * -kBackSpeed, ForceMode.Force);
    }

    override protected void SerchTarget()//�^�[�Q�b�g�̋����ƕ�����T��
    {
        //�^�[�Q�b�g�������ꂽ��
        m_isHitSearch = false;
        //�ł��߂��^�[�Q�b�g��T��
        Vector3 myPos = transform.position;
        float shortDistance = 100000;//�K���Ȓl
        for (int i = 0; i < m_targetList.Length; ++i)
        {
            //���g���Ȃ��Ȃ��΂�
            if (m_targetList[i] == null) continue;
            if (m_targetList[i].GetComponent<PlayerBase>().IsDeath()) continue;
            //����Ɍ������x�N�g��
            Vector3 vec = m_targetList[i].transform.position - myPos;
            vec.y = 0.0f;//�c�����͍l�����Ȃ�
                         //�^���N�̏ꍇ
            if (m_targetList[i].tag == "Tank")
            {
                //�^���N���X�L���𔭓����Ă���Ȃ�
                if (m_targetList[i].GetComponent<Tank>().IsSkilling())
                {
                    //���łɂق��̃^���N���^�[�Q�b�g�ɂȂ��Ă���Ȃ�߂��ق���D�悷��
                    if (m_isHitSearch)
                    {
                        //�ŒZ�����Ȃ�
                        if (shortDistance > vec.magnitude)
                        {
                            //���݂̍ŒZ�ɂ���
                            shortDistance = vec.magnitude;
                            //�^�[�Q�b�g�ւ̃x�N�g��
                            m_targetDir = vec;
                            //�^�[�Q�b�g�ɂ���
                            m_target = m_targetList[i];
                            m_isHitSearch = true;
                            continue;
                        }
                    }
                    //�^�[�Q�b�g�ւ̃x�N�g��
                    m_targetDir = vec;
                    //�^�[�Q�b�g�ɂ���
                    m_target = m_targetList[i];
                    m_isHitSearch = true;
                    continue;
                }
            }
            //�ŒZ�����Ȃ�
            if (shortDistance > vec.magnitude)
            {
                //���̍ŒZ�����ɂ���
                shortDistance = vec.magnitude;
                //����
                m_targetDir = vec.normalized;//���K��

                m_isHitSearch = true;
            }
        }
        m_targetDis = shortDistance;//�ŒZ������ۑ�
    }
    override protected void AttackCoolTime()
    {
        //�N�[���^�C����i�߂�
        m_attackCoolTime -= Time.deltaTime;
        if (m_attackCoolTime <= 0.0f)
        {
            m_attackCoolTime = 0.0f;
        }
    }
    public void MagicShot()//�e��ł�
    {
        //�e�̐���
        GameObject magicShot = Instantiate(m_magicShotPrefab, m_leftHand.transform.position, Quaternion.identity);
        //���W�b�h�{�f�B���擾
        Rigidbody shotRb = magicShot.GetComponent<Rigidbody>();
        //�e�̈ړ�����
        if(m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();
        }
        //�e�̈ړ�
        shotRb.AddForce(m_targetDir * kShotSpeed, ForceMode.Impulse);
        //�_���[�W��ݒ肷��
        magicShot.GetComponent<AttackPower>().damage = kDamage;
    }
    private void UpdateIdle()//�ҋ@
    {
        Debug.Log("Wizard��Idle���\n");
        //���f���̌����X�V
        base.ModelDir();

        //�t�F�[�h���͉������Ȃ�
        if (m_transitionFade.IsFadeNow())
        {
            return;
        }

        //�߂�����Ȃ痣���
        if (m_targetDis <= kBackDis && m_backCoolTime <= 0.0f)
        {
            ChangeState(StateType.Back);
            return;
        }
        //�U��
        if (m_attackCoolTime <= 0.0f)
        {
            ChangeState(StateType.Attack);
            return;
        }
        //�����Ȃ�߂Â�
        if (m_targetDis > kChaseDis)
        {
            ChangeState(StateType.Chase);
            return;
        }
    }
    private void UpdateChase()//�ǂ�������
    {
        Debug.Log("Warrior��Chase���\n");
        //�߂Â�����
        if (m_targetDis < kChaseDis)
        {
            ChangeState(StateType.Idle);
            return;
        }
        //�ړ�
        if (m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();//���K��
        }
        //�ːi
        Vector3 moveVec = transform.rotation * Vector3.forward * kChaseSpeed * Time.deltaTime;
        m_rb.AddForce(moveVec, ForceMode.Force);
        //���f���̌����X�V
        base.ModelDir();
    }
    private void UpdateBack()//������
    {
        BackForce();
        Debug.Log("Wizard��Back���\n");
        if (m_isFinishAnimBack)
        {
            ChangeState(StateType.Idle);
            return;
        }

    }
    private void UpdateAttack()//�U��
    {
        Debug.Log("Wizard��Attack���\n");
        //���f���̌����X�V
        base.ModelDir();
        //�A�j���[�V�������I��������
        if (m_isFinishAttackAnim)
        {
            m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateHit()//����
    {
        Debug.Log("Wizard��Hit���\n");
        if (m_isFinishHitAnim)
        {
            m_isFinishHitAnim = false;
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateDead()//���S
    {
        Debug.Log("Wizard��Dead���\n");
    }
    override protected void ChangeState(StateType state)
    {
        switch (state)
        {
            //�ҋ@���
            case StateType.Idle:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Back", false);
                m_animator.SetBool("Hit", false);
                m_animator.SetBool("Dead", false);
                break;
            //�ǂ�������
            case StateType.Chase:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", true);
                m_animator.SetBool("Back", false);
                m_animator.SetBool("Hit", false);
                m_animator.SetBool("Dead", false);
                break;
            //������
            case StateType.Back:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Back", true);
                m_animator.SetBool("Hit", false);
                m_animator.SetBool("Dead", false);
                //�o�b�N�X�e�b�v�̃N�[���^�C��
                m_backCoolTime = kBackCoolTime;
                m_isFinishAnimBack = false;
                break;
            //�U��
            case StateType.Attack:
                m_animator.SetBool("Attack", true);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Back", false);
                m_animator.SetBool("Hit", false);
                m_animator.SetBool("Dead", false);
                m_isFinishAttackAnim = false;   
                break;
            //����
            case StateType.Hit:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Back", false);
                m_animator.SetBool("Hit", true);
                m_animator.SetBool("Dead", false);
                m_isFinishHitAnim = false;
                break;
            //���S
            case StateType.Dead:
                m_animator.SetBool("Attack", false);
                m_animator.SetBool("Chase", false);
                m_animator.SetBool("Back", false);
                m_animator.SetBool("Hit", false);
                m_animator.SetBool("Dead", true);
                break;
        }
        m_nextState = state;
    }

    override protected void UpdateState()
    {
        //�������[�v��h��
        int count = 0;
        do
        {
            //���̏�Ԃɕω�
            m_nowState = m_nextState;
            switch (m_nowState)
            {
                //�ҋ@���
                case StateType.Idle:
                    UpdateIdle();
                    break;
                case StateType.Chase:
                    UpdateChase();
                    break;
                //������
                case StateType.Back:
                    UpdateBack();
                    break;
                //�U��
                case StateType.Attack:
                    UpdateAttack();
                    break;
                //����
                case StateType.Hit:
                    UpdateHit();
                    break;
                //���S
                case StateType.Dead:
                    UpdateDead();
                    break;
            }

            //�J�E���g�𐔂���
            count++;
            if (count > 10) break;//���[�v�𔲂���

        } while (m_nextState == m_nowState);//��Ԃ��ω����Ă��Ȃ��Ȃ烋�[�v�𔲂���
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        //�o�b�N�X�e�b�v�̃N�[���^�C��
        m_backCoolTime -= Time.deltaTime;
        if(m_backCoolTime <= 0.0f) m_backCoolTime = 0.0f;
        //���S���Ă�����
        if (m_isDead)
        {
            ChangeState(StateType.Dead);
        }
        //�ǂ�������^�[d�Q�b�g�����Ȃ��Ȃ�ҋ@��Ԃɂ���
        else if (!m_isHitSearch)
        {
            ChangeState(StateType.Idle);
        }
        //��Ԃɍ��킹������
        UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_isDead) return;//���S���Ă����牽�����Ȃ�
        //�U�����ꂽ�Ƃ�
        if (other.tag == "PlayerMeleeAttack" || other.tag == "PlayerRangeAttack")
        {
            //�̗͂����炷
            m_characterStatus.hitPoint -= other.GetComponent<AttackPower>().damage;
            //�̗͂�0�ȉ��Ȃ玀�S
            if (m_characterStatus.hitPoint <= 0)
            {
                m_isDead = true;
                m_characterStatus.hitPoint = 0; // �̗͂�0�ɂ���
                return;
            }
            //�̂�����
            Vector3 knokcBack = this.transform.position - other.transform.position;
            knokcBack.y = 0.0f; // �c�����͍l�����Ȃ�
            knokcBack.Normalize(); // ���K��
            m_rb.AddForce(knokcBack * kKnockBackForce, ForceMode.Impulse);
          
            //�q�b�g�A�j���[�V�������ɂ܂�����ꂽ��ŏ�����
            if (m_nowState == StateType.Hit)
            {
                //�ŏ�����Đ�
                m_animator.Play("Wizard_Hit", 0, 0);
            }
            else
            {
                //���ꃊ�A�N�V����
                ChangeState(StateType.Hit);
                return;
            }
        }
    }
}
