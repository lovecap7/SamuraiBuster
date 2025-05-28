using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    PlayerBase m_player;

    Image m_fill;
    Tweener m_shake;

    const float kCautionRatio = 0.5f;
    const float kDangerRatio  = 0.2f;

    readonly Color32 kNomal   = new(100,200,100,255);
    readonly Color32 kCaution = new(240,240, 50,255);
    readonly Color32 kDanger  = new(200, 50, 30,255);

    public void SetPlayer(in PlayerBase player)
    {
        m_player = player;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_fill = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        // ���t���[��HP�������m�F����
        float ratio = m_player.GetHitPointRatio();

        // �l���ω����Ă�����
        if (Mathf.Abs(ratio - m_fill.fillAmount) < 0.01f) return;

        // �񕜂̏ꍇ�A
        if (ratio - m_fill.fillAmount > 0)
        {
            HealAnim();
        }
        else
        {
            DamageAinm();
        }

        // ���f
        m_fill.fillAmount = ratio; 
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
            m_shake = transform.DOShakePosition(1.0f, 6.0f);
        }
        else if (ratio > kDangerRatio)
        {
            m_fill.color = kCaution;
            m_shake = transform.DOShakePosition(1.0f, 16.0f);
        }
        else if (ratio <= kDangerRatio)
        {
            m_fill.color = kDanger;
            m_shake = transform.DOShakePosition(1.0f, 26.0f);
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
