using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectDirector : MonoBehaviour
{
    void SceneTransition()
    {
        SceneManager.LoadScene("TitleScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 2"))
        {
            SceneTransition();
        }
    }
}
