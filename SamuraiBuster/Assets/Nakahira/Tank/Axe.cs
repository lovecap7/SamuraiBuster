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
        // ブラックリストとホワイトリスト、どっちが楽か…
        // 敵、壁に当たったら火花を散らす
        foreach (string hitTag in m_hitTags)
        {
            if (!other.CompareTag(hitTag)) return;
        }

        Instantiate(m_hitEffect, transform.position, transform.rotation);
    }
}
