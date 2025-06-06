using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField]
    GameObject m_hitEffect;

    private readonly string[] m_hitTags =
    {
        "RangeEnemy",
        "MeleeEnemy",
        "Boss",
        "Wall"
    };

    private void OnTriggerEnter(Collider other)
    {
        // �u���b�N���X�g�ƃz���C�g���X�g�A�ǂ������y���c
        // �G�A�ǂɓ���������ΉԂ��U�炷
        foreach (string hitTag in m_hitTags)
        {
            if (!other.CompareTag(hitTag)) return;
        }

        Instantiate(m_hitEffect, transform.position, transform.rotation);
    }
}
