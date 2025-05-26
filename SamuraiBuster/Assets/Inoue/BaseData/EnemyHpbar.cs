using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpbar : MonoBehaviour
{
    private Slider m_slider;
    private CharacterStatus m_status;
    private GameObject m_camera;
    // Start is called before the first frame update
    void Start()
    {
        m_slider = GetComponent<Slider>();
        GameObject owner = transform.parent.parent.gameObject;
        m_status = owner.GetComponent<CharacterStatus>();
        //カメラのほうを常に向くようにする
        m_camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = m_camera.transform.rotation; ; //カメラのほうを向く
        m_slider.value = m_status.hitPoint; //体力を更新
    }
}
