using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    [SerializeField]
    PlayerBase m_player;

    Image m_fill;
    bool m_isMax = false;

    // 0からMaxまでだんだん彩度が上がっていく感じ
    readonly Color32 kInitColor = new(255, 255, 255, 255);
    readonly Color32 kMaxColor  = new(  0, 183, 255, 255);

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
        // 毎フレームHP割合を確認する
        float ratio = m_player.GetSkillChargeRatio();
        float rRatio = 1.0f - ratio;

        // 反映
        m_fill.fillAmount = ratio;

        // 色変え
        // 線形補完はおろか掛け算すらめんどくさい
        Color tempColor = new((kInitColor.r * rRatio + kMaxColor.r * ratio),
            (kInitColor.g * rRatio + kMaxColor.g * ratio),
            (kInitColor.b * rRatio + kMaxColor.b * ratio), 255);

        if (ratio < 0.1f)
        {
            m_isMax = false;
        }

        // Maxになったとき、
        if (ratio > 0.99f && m_isMax == false)
        {
            // アニメーション
            transform.DOPunchPosition(new Vector3(0,5.0f,0), 0.1f);
            m_isMax = true;
        }

        m_fill.color = tempColor;
    }
}
