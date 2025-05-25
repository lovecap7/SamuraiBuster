using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    // こいつが攻撃判定を持ちます
    [SerializeField]
    GameObject m_hitExplosion;
    Vector3 m_initPos;
    public Vector3 m_targetPos;
    float m_time;

    const float kMoveSpeed = 0.01f;
    const int kExplosionDamage = 1000;
    const int kExplosionDamageRandomRange = 200;

    // Start is called before the first frame update
    void Start()
    {
        m_initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_time += kMoveSpeed;

        // 線形補完でこう…ツーッとね
        transform.position = Vector3.Lerp(m_initPos, m_targetPos, m_time);

        if (m_time >= 1.0f)
        {
            Explosion();
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 地面に当たったら爆発！
        if (other.CompareTag("Ground"))
        {
            Explosion();
        }
    }

    void Explosion()
    {
        var exp = Instantiate(m_hitExplosion, transform.position, Quaternion.identity);
        // スクリプトが増えるのがめんどくさいのでここで設定
        exp.GetComponent<AttackPower>().damage = kExplosionDamage + (int)Random.Range(kExplosionDamageRandomRange * -0.5f, kExplosionDamageRandomRange * 0.5f);
        Destroy(gameObject);
        return;
    }
}
