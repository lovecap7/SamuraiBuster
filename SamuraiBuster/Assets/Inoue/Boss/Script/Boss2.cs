using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Boss2 : EnemyBase
{
    //�̗�
    [SerializeField] private int kHP = 10000;
    //�����̗�
    [SerializeField] private int kAddHP = 6000;
    //�_���[�W
    [SerializeField] private int kMeleeDamage = 200;
    [SerializeField] private int kTackleDamage = 150;
    //�d���t���[��
    [SerializeField] private float kFreezeFrame = 3.0f;
    //�����ł��鎘�̐�
    [SerializeField] private int kMaxSamuraiNum = 15;//3�̔{���ɂ��Ăق����ł�
    //�^�b�N���̎�������
    [SerializeField] private float kTackleFrame = 40.0f;
    //�^�b�N���̃X�s�[�h
    [SerializeField] private float kTackleSpeed = 2000.0f;
    //�X�R�A
    [SerializeField] private int kScorePoint = 50000;
    //�ǂ������鑬�x
    [SerializeField] private float kChaseSpeed = 1000.0f;
    //�_���[�W���󂯂��Ƃ��ɏ����~�܂�
    private const float kStopFrame = 0.2f;
    private float m_stopFrame;

  
    //�ǂ������鋗��
    private float kChaseDis = 1.4f;
    //�d��
    private float m_freezeTime;
    private AttackPower m_meleePower;
    private AttackPower m_tacklePower;
    //�U������
    [SerializeField] private GameObject m_rightHand;
    private CapsuleCollider m_rightHandCollider;
    [SerializeField] private GameObject m_tackle;
    private SphereCollider m_tackleCollider;
    //���肩�玘
    [SerializeField] private GameObject m_leftHand;
    //��
    [SerializeField] private GameObject m_samurai;
    //�����ł��鎘�̐�
    private int m_samuraiNum = 0;
    //�`���[�W�G�t�F�N�g
    [SerializeField] private GameObject m_chargeEff;
    //�^�b�N�����̃G�t�F�N�g
    [SerializeField] private GameObject m_tackleEff;
    //�q�b�g�G�t�F�N�g
    [SerializeField] private GameObject m_hitEffect;
 
    //�����[�A�^�b�N
    private bool m_isMeleeAttack = false;
    //�^�b�N��
    private bool m_isTackleAttack = false;
    //�����W�A�^�b�N
    private bool m_isRangeAttack = false;
    //�K�E�Z
    private bool m_isUltAttack = false;

    //�^�b�N���`���[�W����
    private bool m_isChargeCmp = false;
    //�^�b�N���t���[�����J�E���g
    private float m_tackleTime;
    //�e�̑��x
    private const float kShotSpeed = 5.0f;


    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        //�l���������ꍇ�����̗͂�������
        int addHp = 0;
        if (m_targetList.Length > 2) addHp = kAddHP;
        m_characterStatus.hitPoint = kHP + addHp;
        //�̗̓o�[�ɐݒ�
        Slider hpBar = transform.Find("Canvas_Hp/Hpbar").gameObject.GetComponent<Slider>();
        hpBar.maxValue = m_characterStatus.hitPoint;

        //�U������
        m_rightHandCollider = m_rightHand.GetComponent<CapsuleCollider>();
        m_rightHandCollider.enabled = false;
        m_meleePower = m_rightHand.GetComponent<AttackPower>();
        m_meleePower.damage = 0;

        m_tackleCollider = m_tackle.GetComponent<SphereCollider>();
        m_tackleCollider.enabled = false;
        m_tacklePower = m_tackle.GetComponent<AttackPower>();
        m_tacklePower.damage = 0;

        //�d���t���[��
        m_freezeTime = kFreezeFrame;
        //�^�b�N���t���[��
        m_tackleTime = kTackleFrame;
        //�_���[�W���󂯂��ۂ̍d��
        m_stopFrame = 0.0f;

        m_chargeEff.SetActive(false);//�`���[�W�G�t�F�N�g�͔�\��
        m_tackleEff.SetActive(false);//�^�b�N���G�t�F�N�g�͔�\��

        //�X�R�A
        m_scorePoint = kScorePoint;
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
                //���݂̍ŒZ�ɂ���
                shortDistance = vec.magnitude;
                //�^�[�Q�b�g�ւ̃x�N�g��
                m_targetDir = vec;
                //�^�[�Q�b�g�ɂ���
                m_target = m_targetList[i];
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
    public void OnActivemMeleeAttack()
    {
        m_rightHandCollider.enabled = true;
        //�_���[�W��ݒ�
        m_meleePower.damage = kMeleeDamage;
    }
    public void OffActivemMeleeAttack()
    {
        m_rightHandCollider.enabled = false;
        //�_���[�W�����Z�b�g
        m_meleePower.damage = 0;
    }
    public void OnActivemTackleAttack()
    {
        m_tackleCollider.enabled = true;
        //�_���[�W��ݒ�
        m_tacklePower.damage = kTackleDamage;
    }
    public void OffActivemTackleAttack()
    {
        m_tackleCollider.enabled = false;
        //�_���[�W�����Z�b�g
        m_tacklePower.damage = 0;
    }
    public void OnChargeCmp()//�^�b�N���̃`���[�W������������
    {
        m_isChargeCmp = true;
    }
    public void InstanseSamurai()
    {
        //�����ł��邩�̃`�F�b�N
        if(m_samuraiNum <= kMaxSamuraiNum)
        {
            //Wave�̎q�I�u�W�F�N�g�Ƃ��Đ�������
            GameObject samurai = Instantiate(m_samurai, m_leftHand.transform.position, Quaternion.identity,transform.parent);
            //���W�b�h�{�f�B���擾
            Rigidbody shotRb = samurai.GetComponent<Rigidbody>();
            //�e�̈ړ�����
            if (m_targetDir.magnitude > 0.0f)
            {
                m_targetDir.Normalize();
            }
            //�e�̈ړ�
            shotRb.AddForce(m_targetDir * kShotSpeed, ForceMode.Impulse);
            ++m_samuraiNum;
        }
    }
    private void UpdateIdle()
    {
        Debug.Log("Boss��Idle���\n");
        //���f���̌����X�V
        base.ModelDir();
        //�t�F�[�h���͉������Ȃ�
        if (m_transitionFade.IsFadeNow())
        {
            return;
        }
        //�U��
        if (m_attackCoolTime <= 0.0f)
        {
            //�����_���łǂꂩ�I��
            int seleceAttack;
            seleceAttack = UnityEngine.Random.Range(0, 7);
            if (seleceAttack >= 0 && seleceAttack < 3)
            {
                m_isRangeAttack = true;
            }
            else if (seleceAttack >= 3 && seleceAttack < 5)
            {
                m_isTackleAttack = true;
            }
            else
            {
                m_isMeleeAttack = true;
            }
            ChangeState(StateType.Attack);
            return;
        }
        //�߂Â�����
        if (m_targetDis >= kChaseDis)
        {
            ChangeState(StateType.Chase);
            return;
        }
    }
    private void UpdateChase()
    {
        Debug.Log("Boss��Chase���\n");
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
        //�U��
        if (m_attackCoolTime <= 0.0f)
        {
            //�����_���łǂꂩ�I��
            int seleceAttack;
            seleceAttack = UnityEngine.Random.Range(0, 7);
            if (seleceAttack >= 0 && seleceAttack < 3)
            {
                m_isRangeAttack = true;
            }
            else if (seleceAttack >= 3 && seleceAttack < 5)
            {
                m_isTackleAttack = true;
            }
            else
            {
                m_isMeleeAttack = true;
            }
            ChangeState(StateType.Attack);
            return;
        }
        //�߂Â�����
        if (m_targetDis < kChaseDis)
        {
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateAttack()
    {
        Debug.Log("Boss��Attack���\n");
        if (m_isMeleeAttack)
        {
            //�����[�A�^�b�N
            UpdateMeleeA();
        }
        else if (m_isTackleAttack)
        {
            //�^�b�N��
            UpdateTackleA();
        }
        else if (m_isRangeAttack)
        {
            //�����W�A�^�b�N
            UpdateRangeA();
        }
        else if (m_isUltAttack)
        {
            //�K�E�Z
            UpdateUltA();
        }
    }
    private void UpdateFreeze()
    {
        Debug.Log("Boss��Freeze���\n");
        m_freezeTime -= Time.deltaTime;
        if (m_freezeTime <= 0.0f)
        {
            m_freezeTime = kFreezeFrame;
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateDead()
    {
        Debug.Log("Boss��Dead���\n");
    }

    private void UpdateMeleeA()
    {
        //�A�j���[�V�������I��������
        if (m_isFinishAttackAnim)
        {
            //���Z�b�g
            m_isMeleeAttack = false;
            m_isTackleAttack = false;
            m_isRangeAttack = false;
            m_isUltAttack = false;
            m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateTackleA()
    {
        //���f���̌����X�V
        base.ModelDir();
        m_chargeEff.SetActive(true);//�`���[�W�G�t�F�N�g��\��
        //�`���[�W������������
        if (m_isChargeCmp)
        {
            m_tackleEff.SetActive(true);//�^�b�N���G�t�F�N�g��\��
            m_tackleTime -= Time.deltaTime;
            //�ړ�
            if (m_targetDir.magnitude > 0.0f)
            {
                m_targetDir.Normalize();//���K��
            }
            //�ːi
            Vector3 moveVec = transform.rotation * Vector3.forward * kTackleSpeed * Time.deltaTime;
            m_rb.AddForce(moveVec, ForceMode.Force);
            //�^�b�N���̎������I�������
            if (m_tackleTime <= 0.0f)
            {
                //���Z�b�g
                m_isMeleeAttack = false;
                m_isTackleAttack = false;
                m_isRangeAttack = false;
                m_isUltAttack = false;
                m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
                m_tackleTime = kTackleFrame;
                m_isChargeCmp = false;
                ChangeState(StateType.Freeze);
                return;
            }
        }
    }
    private void UpdateRangeA()
    {
        //���f���̌����X�V
        base.ModelDir();
        //�A�j���[�V�������I��������
        if (m_isFinishAttackAnim)
        {
            //���Z�b�g
            m_isMeleeAttack = false;
            m_isTackleAttack = false;
            m_isRangeAttack = false;
            m_isUltAttack = false;
            m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
            ChangeState(StateType.Idle);
            return;
        }
    }
    private void UpdateUltA()
    {
        //�A�j���[�V�������I��������
        if (m_isFinishAttackAnim)
        {
            //���Z�b�g
            m_isMeleeAttack = false;
            m_isTackleAttack = false;
            m_isRangeAttack = false;
            m_isUltAttack = false;
            m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
            ChangeState(StateType.Idle);
            return;
        }
    }

    override protected void ChangeState(StateType state)
    {
        switch (state)
        {
            //�ҋ@���
            case StateType.Idle:
                m_animator.SetBool("MeleeA", false);
                m_animator.SetBool("TackleA", false);
                m_animator.SetBool("RangeA", false);
                m_animator.SetBool("Freeze", false);
                m_animator.SetBool("Dead", false);
                m_animator.SetBool("Chase", false);
                //���Z�b�g
                m_isMeleeAttack = false;
                m_isTackleAttack = false;
                m_isRangeAttack = false;
                m_isUltAttack = false;
                m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
                m_tackleTime = kTackleFrame;
                m_isChargeCmp = false;
                OffActivemTackleAttack();
                break;
            //�ړ�
            case StateType.Chase:
                m_animator.SetBool("Chase", true);
                m_animator.SetBool("MeleeA", false);
                m_animator.SetBool("TackleA", false);
                m_animator.SetBool("RangeA", false);
                m_animator.SetBool("Freeze", false);
                m_animator.SetBool("Dead", false);
                //���Z�b�g
                m_isMeleeAttack = false;
                m_isTackleAttack = false;
                m_isRangeAttack = false;
                m_isUltAttack = false;
                m_attackCoolTime = kAttackCoolTime;//�N�[���^�C��
                m_tackleTime = kTackleFrame;
                m_isChargeCmp = false;
                OffActivemTackleAttack();
                break;
            //�U��
            case StateType.Attack:
                OffActivemMeleeAttack();
                OffActivemTackleAttack();
                if (m_isMeleeAttack)
                {
                    //�����[�A�^�b�N
                    m_animator.SetBool("MeleeA", true);
                    m_animator.SetBool("TackleA", false);
                    m_animator.SetBool("RangeA", false);
                    m_animator.SetBool("Freeze", false);
                    m_animator.SetBool("Dead", false);
                    m_animator.SetBool("Chase", false);
                }
                else if (m_isTackleAttack)
                {
                    //�^�b�N��
                    m_tackleTime = kTackleFrame;
                    m_isChargeCmp = false;
                    m_animator.SetBool("MeleeA", false);
                    m_animator.SetBool("TackleA", true);
                    m_animator.SetBool("RangeA", false);
                    m_animator.SetBool("Freeze", false);
                    m_animator.SetBool("Dead", false);
                    m_animator.SetBool("Chase", false);
                }
                else if (m_isRangeAttack)
                {
                    //�����W�A�^�b�N
                    m_animator.SetBool("MeleeA", false);
                    m_animator.SetBool("TackleA", false);
                    m_animator.SetBool("RangeA", true);
                    m_animator.SetBool("Freeze", false);
                    m_animator.SetBool("Dead", false);
                    m_animator.SetBool("Chase", false);
                }
                else if (m_isUltAttack)
                {
                    //�K�E�Z
                }
                m_isFinishAttackAnim = false;
                m_attackCoolTime = kAttackCoolTime;
                break;
            //�d��
            case StateType.Freeze:
                m_animator.SetBool("MeleeA", false);
                m_animator.SetBool("TackleA", false);
                m_animator.SetBool("Freeze", true);
                m_animator.SetBool("Dead", false);
                m_animator.SetBool("Chase", false);
                m_chargeEff.SetActive(false);
                m_tackleEff.SetActive(false);
                break;
            //���S
            case StateType.Dead:
                m_animator.SetBool("MeleeA", false);
                m_animator.SetBool("TackleA", false);
                m_animator.SetBool("Freeze", true);
                m_animator.SetBool("Dead", true);
                m_animator.SetBool("Chase", false);
                m_chargeEff.SetActive(false);
                m_tackleEff.SetActive(false);
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
                //�ǂ�������
                case StateType.Chase:
                    UpdateChase();
                    break;
                //�U��
                case StateType.Attack:
                    UpdateAttack();
                    break;
                //�d��
                case StateType.Freeze:
                    UpdateFreeze();
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
        //���S���Ă�����
        if (m_isDead)
        {
            ChangeState(StateType.Dead);
            return;
        }
        //�d�����Ă���Ȃ�
        m_stopFrame -= Time.deltaTime;
        if (m_stopFrame <= 0.0f)
        {
            m_stopFrame = 0.0f;//���Z�b�g
            m_animator.speed = 1.0f;
        }
        //�ǂ�������^�[�Q�b�g�����Ȃ��Ȃ�ҋ@��Ԃɂ���
        if (!m_isHitSearch)
        {
            ChangeState(StateType.Idle);
        }
        //��Ԃɍ��킹������
        UpdateState();

        Debug.Log($"Boss��HP{m_characterStatus.hitPoint}");
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
                m_animator.speed = 1.0f;
                m_isDead = true;
                m_characterStatus.hitPoint = 0; // �̗͂�0�ɂ���
                return;
            }
            //�_���[�W���󂯂���d��
            m_rb.velocity = Vector3.zero;
            m_animator.speed = 0.0f;
            m_stopFrame = kStopFrame; // �d���t���[����ݒ�
        }
        //�U���𓖂Ă��Ƃ�
        if (m_nowState == StateType.Attack)
        {
            //�U���𓖂Ă��Ƃ�
            if (other.tag == "Fighter" ||
               other.tag == "Mage" ||
               other.tag == "Healer" ||
               other.tag == "Tank")
            {
                if (m_isMeleeAttack)
                {
                    //�q�b�g�G�t�F�N�g���o��
                    GameObject hitEffect = Instantiate(m_hitEffect, m_rightHand.transform.position, Quaternion.identity);
                }
                else if (m_isTackleAttack)
                {
                    GameObject hitEffect = Instantiate(m_hitEffect, m_tackle.transform.position, Quaternion.identity);
                }
            }
        }
    }

    private void OnDestroy()
    {
        GameObject wave = transform.parent.gameObject;
        wave.GetComponent<Wave>().AllEnemyDead();
    }
}
