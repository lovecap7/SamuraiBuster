using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Boss : EnemyBase
{
    //�̗�
    [SerializeField] private const int kHP = 8500;
    //�����̗�
    [SerializeField] private int kAddHP = 6000;
    //�_���[�W
    [SerializeField] private int kMeleeDamage = 110;
    [SerializeField] private int kMagicDamage = 110;
    [SerializeField] private int kTackleDamage = 100;
    //�e�̑��x
    [SerializeField] private float kShotSpeed = 5.0f;
    //�^�b�N���̎�������
    [SerializeField] private float kTackleFrame = 40.0f;
    //�^�b�N���̃X�s�[�h
    [SerializeField] private float kTackleSpeed = 1500.0f;
    //�d������
    [SerializeField] private float kFreezeFrame = 3.0f;
    //�X�R�A
    [SerializeField] private int kScorePoint = 12000;


    private AttackPower m_meleePower;
    private AttackPower m_tacklePower;
    //�U������
    [SerializeField] private GameObject m_rightHand;
    private CapsuleCollider m_rightHandCollider;
    [SerializeField] private GameObject m_tackle;
    private SphereCollider m_tackleCollider;
    //���肩�疂�@���o��
    [SerializeField] private GameObject m_leftHand;
    //�e
    [SerializeField] private GameObject m_magicShotPrefab;
    //�`���[�W�G�t�F�N�g
    [SerializeField] private GameObject m_chargeEff;
    //�^�b�N�����̃G�t�F�N�g
    [SerializeField] private GameObject m_tackleEff;
    //�q�b�g�G�t�F�N�g
    [SerializeField] private GameObject m_hitEffect;
   
    //�d���t���[��
    private float m_freezeTime;
    //�_���[�W���󂯂��Ƃ��ɏ����~�܂�
    private const float kStopFrame = 0.2f;
    private float m_stopFrame;

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
    //�^�b�N���̎�������
    private float m_tackleTime;

    [SerializeField] private AudioClip m_attackHitSE; //�U���q�b�gSE
    [SerializeField] private AudioClip m_chargeSE; //�`���[�WSE
    [SerializeField] private AudioClip m_magicSE; //���@SE
    [SerializeField] private AudioClip m_damageSE;    //�_���[�WSE

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
        m_isChargeCmp= true;
    }
    public void MagicShot()//�e��ł�
    {
        //�e�̐���
        GameObject magicShot = Instantiate(m_magicShotPrefab, m_leftHand.transform.position, Quaternion.identity);
        //���W�b�h�{�f�B���擾
        Rigidbody shotRb = magicShot.GetComponent<Rigidbody>();
        //�e�̈ړ�����
        if (m_targetDir.magnitude > 0.0f)
        {
            m_targetDir.Normalize();
        }
        //�e�̈ړ�
        shotRb.AddForce(m_targetDir * kShotSpeed, ForceMode.Impulse);
        //�_���[�W��ݒ�
        magicShot.GetComponent<AttackPower>().damage = kMagicDamage;
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
            int seleceAttack = Random.Range(0, 3);
            if (seleceAttack <= 0)
            {
                m_isMeleeAttack = true;
            }
            else if(seleceAttack == 1)
            {
                m_isTackleAttack = true;
            }
            else if (seleceAttack == 2)
            {
                m_isRangeAttack = true;
            }
            ChangeState(StateType.Attack);
            return;
        }
    }
    private void UpdateRun()
    {
        Debug.Log("Boss��Run���\n");
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
        if(m_freezeTime <= 0.0f)
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
            Vector3 moveVec = transform.rotation * Vector3.forward * kTackleSpeed * Time.deltaTime ;
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
    //�U���q�b�gSE���Đ�����
    private void PlayAttackHitSE()
    {
        if (m_audioSource != null && m_attackHitSE != null)
        {
            m_audioSource.PlayOneShot(m_attackHitSE);
        }
    }

    //�_���[�WSE���Đ�����
    private void PlayDamageSE()
    {
        if (m_audioSource != null && m_damageSE != null)
        {
            m_audioSource.PlayOneShot(m_damageSE);
        }
    }
    //���@SE���Đ�����
    private void PlayMagicSE()
    {
        if (m_audioSource != null && m_magicSE != null)
        {
            m_audioSource.PlayOneShot(m_magicSE);
        }
    }
    //�`���[�WSE���Đ�����
    private void PlayChargeSE()
    {
        if (m_audioSource != null && m_chargeSE != null)
        {
            m_audioSource.PlayOneShot(m_chargeSE);
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
                }
                else if(m_isTackleAttack)
                {
                    PlayChargeSE(); //�`���[�WSE���Đ�
                    //�^�b�N��
                    m_tackleTime = kTackleFrame;
                    m_isChargeCmp = false;
                    m_animator.SetBool("MeleeA", false);
                    m_animator.SetBool("TackleA", true);
                    m_animator.SetBool("RangeA", false);
                    m_animator.SetBool("Freeze", false);
                    m_animator.SetBool("Dead", false);
                }
                else if(m_isRangeAttack)
                {
                    PlayMagicSE(); //���@SE���Đ�
                    //�����W�A�^�b�N
                    m_animator.SetBool("MeleeA", false);
                    m_animator.SetBool("TackleA", false);
                    m_animator.SetBool("RangeA", true);
                    m_animator.SetBool("Freeze", false);
                    m_animator.SetBool("Dead", false);
                }
                else if(m_isUltAttack)
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
                m_chargeEff.SetActive(false);
                m_tackleEff.SetActive(false);
                break;
            //���S
            case StateType.Dead:
                m_animator.SetBool("MeleeA", false);
                m_animator.SetBool("TackleA", false);
                m_animator.SetBool("Freeze", true);
                m_animator.SetBool("Dead", true);
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
                    UpdateRun();
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
            PlayDamageSE(); //�_���[�WSE���Đ�

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
                //�U���q�b�gSE���Đ�
                PlayAttackHitSE();
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
}
