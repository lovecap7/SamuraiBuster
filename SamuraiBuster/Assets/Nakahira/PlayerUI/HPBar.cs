using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    PlayerBase m_player;

    Slider m_slider;
    Image m_fill;

    const float kCautionRatio = 0.5f;
    const float kDangerRatio  = 0.2f;

    readonly Color32 kNomal   = new(100,200,100,255);
    readonly Color32 kCaution = new(240,240, 50,255);
    readonly Color32 kDanger  = new(200, 50, 30,255);

    public void SetPlayer(ref PlayerBase player)
    {
        m_player = player;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_slider = GetComponent<Slider>();
        m_fill = transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        // ���t���[��HP�������m�F����
        float ratio = m_player.GetHitPointRatio();

        // �l���ω����Ă�����
        if (Mathf.Abs(ratio - m_slider.value) < 0.01f) return;

        // ���f
        m_slider.value = ratio;
    }

    // ����HP�̊�������AHP�Q�[�W�̐F��ς��܂��B
    // ���łɗh�炵�܂��B
    public void ChangeHPColor()
    {
        float ratio = m_slider.value;

        if (ratio > kCautionRatio)
        {
            m_fill.color = kNomal;
            transform.DOShakePosition(1.0f, 3.0f);
        }
        else if (ratio > kDangerRatio)
        {
            m_fill.color = kCaution;
            transform.DOShakePosition(1.0f, 8.0f);
        }
        else if (ratio < kDangerRatio)
        {
            m_fill.color = kDanger;
            transform.DOShakePosition(1.0f, 13.0f);
        }
    }
}
