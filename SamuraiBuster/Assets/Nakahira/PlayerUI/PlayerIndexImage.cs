using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1~4P‚Ì•\¦‚Ì‚Ó‚é‚Ü‚¢
public class PlayerIndexImage : MonoBehaviour
{
    Camera m_camera;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // ‚¸‚Á‚ÆƒJƒƒ‰‚ÉŒü‚©‚¤
        transform.rotation = m_camera.transform.rotation;
    }
}
