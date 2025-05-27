using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour
{
    [SerializeField] private float m_liveTime = 3.0f;
    [SerializeField] private GameObject m_hitEffect;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        m_liveTime -= Time.deltaTime;
        if(m_liveTime <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //敵に当たったら消える
        if (other.tag == "Fighter"  ||
            other.tag == "Mage"     ||
            other.tag == "Tank"     ||
            other.tag == "Healer"   ||
            other.tag == "Assassin")
        {
            //ヒットエフェクトを生成
            GameObject hitEffect = Instantiate(m_hitEffect, transform.position, Quaternion.identity);
            Destroy(hitEffect, 3.0f);

            Destroy(this.gameObject);
        }
    }
}
