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

    const float kCautionRatio = 0.5f;
    const float kDangerRatio  = 0.2f;

    readonly Color kNomal   = new( 0.2f,  0.6f, 0.2f, 1.0f);
    readonly Color kCaution = new(0.95f, 0.95f, 0.2f, 1.0f);
    readonly Color kDanger  = new( 0.8f,  0.2f, 0.1f, 1.0f);

    public void SetPlayer(in PlayerBase player)
    {
        m_player = player;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_fill = transform.GetChild(0).GetComponent<Image>();
        m_parent = transform.parent.gameObject;
    }

    private void Update()
    {
        if (m_player == null) return;

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
    }

    void DeathUpdate()
    {

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
