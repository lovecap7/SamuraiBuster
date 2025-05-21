using Unity.VisualScripting;
using UnityEngine;

public class Fighter : PlayerBase
{
    [SerializeField] GameObject m_katana;
    CapsuleCollider m_katanaCollider;

    // 通常攻撃のクールタイム1~2,2~3段目
    const int kAttackInterval0 = 30;
    const int kAttackInterval1 = 70;
    // 出し切ったor連続攻撃の猶予を過ぎた
    const int kAttackInterval2 = 120;
    int m_attackInterval = 0;
    const int kDodgeInterval = 60;
    const int kInitHP = 100;

    Vector3 kDodgeForce = new(0,0,10.0f);

    int m_dodgeTimer = 0;
    int m_attackTimer = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_katanaCollider = m_katana.GetComponent<CapsuleCollider>();
        m_katanaCollider.enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // タイマー進めて値も制限
        ++m_dodgeTimer;
        if (m_dodgeTimer > kDodgeInterval) m_dodgeTimer = kDodgeInterval;
        ++m_attackTimer;

        // 攻撃のクールタイムを消化していれば
        if (m_attackTimer < m_attackInterval)
        {
            m_anim.SetBool("Attacking", false);
        }

        Debug.Log($"今の攻撃クールタイム:{m_attackInterval - m_attackTimer},回避クールタイム{kDodgeInterval - m_dodgeTimer}");
    }

    public override void Attack()
    {
        // 攻撃のクールタイムが経過していないなら実行しない
        if (m_attackTimer < m_attackInterval) return;

        Debug.Log("通ってる");

        //　今日の クソコード　一日一糞
        var nowState = m_anim.GetCurrentAnimatorStateInfo(0);

        if (nowState.IsName("FighterAtk2")) return;
        else if (nowState.IsName("FighterAtk0")) m_attackInterval = kAttackInterval1;
        else if (nowState.IsName("FighterAtk1")) m_attackInterval = kAttackInterval2;
        else                                     m_attackInterval = kAttackInterval0;

        // 刀を振る
        m_anim.SetBool("Attacking", true);

        // タイマーリセット
        m_attackTimer = 0;
    }

    public override void Skill()
    {
        // ドッジロール
        if (m_dodgeTimer < kDodgeInterval) return;

        m_anim.SetTrigger("Skilling");

        // 今の入力方向にプレイヤーを向ける
        if (m_inputAxis.sqrMagnitude >= 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(new (m_inputAxis.x, 0, m_inputAxis.y));
        }

        m_rigid.AddForce(transform.rotation * kDodgeForce, ForceMode.Impulse);

        m_dodgeTimer = 0; 
    }

    public override void OnDamage(int damage)
    {
        // 無敵ならくらわない
        // これ基底クラスに置いたほうがきれいかも？
        if (m_isInvincibleFrame > 0) return;

        // HPが減る
        m_hitPoint -= damage;

        // ダメージモーション
        m_anim.SetTrigger("Damage");

        // ここが三途の川
        if (m_hitPoint > 0) return;

        // やっぱ死亡モーション
        m_anim.SetTrigger("Death");
    }

    public void EnableKatanaCol()
    {
        m_katanaCollider.enabled = true;
    }

    public void DisableKatanaCol()
    {
        m_katanaCollider.enabled = false;
    }
}
