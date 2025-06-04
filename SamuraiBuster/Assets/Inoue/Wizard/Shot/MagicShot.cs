using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour
{
    [SerializeField] private float m_liveTime = 3.0f;
    [SerializeField] private GameObject m_hitEffect;
    [SerializeField] protected AudioClip m_magicSE; //���@SE
    private AudioSource m_audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //SE���擾
        m_audioSource = GetComponent<AudioSource>();
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
        //�G�ɓ��������������
        if (other.tag == "Fighter"  ||
            other.tag == "Mage"     ||
            other.tag == "Tank"     ||
            other.tag == "Healer"   ||
            other.tag == "Assassin")
        {
            //SE���Đ�
            if (m_audioSource != null)
            {
                if (m_magicSE != null)
                {
                    m_audioSource.clip = m_magicSE;
                }
                m_audioSource.PlayOneShot(m_magicSE);
            }

            //�q�b�g�G�t�F�N�g�𐶐�
            GameObject hitEffect = Instantiate(m_hitEffect, transform.position, Quaternion.identity);
            Destroy(hitEffect, 3.0f);

            Destroy(this.gameObject);
        }
    }
}
