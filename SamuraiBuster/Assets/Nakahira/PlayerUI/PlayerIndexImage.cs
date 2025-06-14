using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 1~4Pの表示のふるまい
public class PlayerIndexImage : MonoBehaviour
{
    Camera m_camera;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;

        SceneManager.sceneLoaded += OnSceneChanged;
    }

    // Update is called once per frame
    void Update()
    {
        // ずっとカメラに向かう
        transform.rotation = m_camera.transform.rotation;
    }

    private void OnSceneChanged(Scene nextScene, LoadSceneMode mode)
    {
        m_camera = Camera.main;
    }
}
