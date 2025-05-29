using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Reflection;

public class FireBall : MonoBehaviour
{
    Rigidbody m_rigidBody;
    Vector3 kInitVel = new(0,0,5.0f);
    int m_timer = 0;
    [SerializeField]
    GameObject m_hitEffect;
    const int kLifeTime = 300;
    Vector3 kAppearSpeed = new(1.0f/ kAppearFrame, 1.0f / kAppearFrame, 1.0f / kAppearFrame);
    const int kAppearFrame = 60;
    const int kAttackPower = 200;
    const int kAttackPowerRandomRange = 100;
    AttackPower m_attackPower;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        // 一直線に飛ぶ それだけ
        m_rigidBody.velocity = transform.rotation * kInitVel;
        GetComponent<ParticleSystem>().Play();
        transform.localScale = Vector3.zero;
        m_attackPower = GetComponent<AttackPower>();
        m_attackPower.damage = kAttackPower + (int)Random.Range(kAttackPowerRandomRange * -0.5f, kAttackPowerRandomRange * 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        ++m_timer;
        // 揺れる
        // 三角関数使うとマジックナンバーが増えてめんどいな
        transform.Translate(transform.rotation * new Vector3(Mathf.Cos(m_timer * 0.1f) *0.01f, Mathf.Sin(m_timer * 0.1f) * 0.01f, 0));

        if (m_timer < kAppearFrame)
        {
            transform.localScale += kAppearSpeed;
        }

        // 寿命を迎えたら消える
        if (m_timer > kLifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MeleeEnemy") || other.CompareTag("RangeEnemy") || other.CompareTag("Boss") || other.CompareTag("Wall"))
        {
            // ヒットエフェクトを出す
            var hit = Instantiate(m_hitEffect);
            hit.transform.position = transform.position;

            // 消える
            Destroy(gameObject);
            return;
        }
    }
}
