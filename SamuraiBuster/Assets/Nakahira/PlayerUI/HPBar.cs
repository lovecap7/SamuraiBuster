using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    PlayerBase m_player;

    Image m_fill;
    Tweener m_shake;
    GameObject m_parent;
    bool m_isDeath = false;

    const float kCautionRatio = 0.5f;
    const float kDangerRatio  = 0.2f;

    readonly Color kNomal   = new( 0.4f,  0.8f, 0.4f, 1.0f);
    readonly Color kCaution = new(1.0f, 1.0f, 0.4f, 1.0f);
    readonly Color kDanger  = new(1.0f,  0.4f, 0.3f, 1.0f);
    readonly Color kDeath   = new(0.5f, 0.5f, 0.6f, 1.0f);

    public void SetPlayer(in PlayerBase player)
    {
        m_player = player;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_fill = transform.GetChild(0).GetComponent<Image>();
        m_parent = transform.parent.gameObject;

        // �����F
        m_fill.color = kNomal;
    }

    private void Update()
    {
        if (m_player == null) return;

        if (!m_player.IsDeath() && m_isDeath)
        {
            m_isDeath = false;
            m_fill.color = kNomal;
        }

        // ���t���[��HP�������m�F����
        float ratio = m_player.GetHitPointRatio();

        // �l���ω����Ă�����
        if (Mathf.Abs(ratio - m_fill.fillAmount) < 0.01f) return;

        // �������̓s���ɂ��L�����Ă���
        bool isHeal = ratio - m_fill.fillAmount > 0;

        // ���f
        m_fill.fillAmount = ratio;

        // �񕜂̏ꍇ
        if (isHeal)
        {
            HealAnim();
        }
        else
        {
            DamageAinm();
        }

        if (m_player.IsDeath() && !m_isDeath)
        {
            m_isDeath = true;
            m_fill.color = kDeath;
        }
    }

    // ����HP�̊�������AHP�Q�[�W�̐F��ς��܂��B
    // ���łɗh�炵�܂��B
    public void DamageAinm()
    {
        float ratio = m_fill.fillAmount;

        m_shake?.Kill();

        if (ratio > kCautionRatio)
        {
            m_fill.color = kNomal;
            m_shake = transform.DOShakePosition(1.0f, 6.0f, 100);
        }
        else if (ratio > kDangerRatio)
        {
            m_fill.color = kCaution;
            m_shake = transform.DOShakePosition(1.0f, 16.0f, 100);
        }
        else if (ratio <= kDangerRatio)
        {
            m_fill.color = kDanger;
            m_shake = transform.DOShakePosition(1.0f, 26.0f, 100);
        }
    }

    public void HealAnim()
    {
        float ratio = m_fill.fillAmount;

        // ����ł��玀�񂾐F�����Ă���̂ŕς��Ȃ�
        if (m_isDeath) return;

        if (ratio > kCautionRatio)
        {
            m_fill.color = kNomal;
        }
        else if (ratio > kDangerRatio)
        {
            m_fill.color = kCaution;
        }
        else if (ratio <= kDangerRatio)
        {
            m_fill.color = kDanger;
        }
    }
}
