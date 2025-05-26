using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField]
    GameObject m_hitEffect;

    private readonly string[] m_ignoreTags =
    {
        "Fighter",
        "Healer",
        "Mage",
        "Tank",
        "Ground"
    };

    private void OnTriggerEnter(Collider other)
    {
        // �u���b�N���X�g�ƃz���C�g���X�g�A�ǂ������y���c
        // �G�A�ǂɓ���������ΉԂ��U�炷
        foreach (string ignTag in m_ignoreTags)
        {
            if (other.CompareTag(ignTag)) return;
        }

        Instantiate(m_hitEffect, transform.position, transform.rotation);
    }
}
