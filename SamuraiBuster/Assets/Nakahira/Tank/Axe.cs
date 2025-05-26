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
        // ブラックリストとホワイトリスト、どっちが楽か…
        // 敵、壁に当たったら火花を散らす
        foreach (string ignTag in m_ignoreTags)
        {
            if (other.CompareTag(ignTag)) return;
        }

        Instantiate(m_hitEffect, transform.position, transform.rotation);
    }
}
