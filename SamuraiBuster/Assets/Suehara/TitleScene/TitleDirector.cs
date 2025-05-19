using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour
{
    void SceneTransition()
    {
        SceneManager.LoadScene("SelectScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            SceneTransition();
        }
        if (Input.GetKeyDown("joystick button 1"))
        {
            SceneTransition();
        }
        if (Input.GetKeyDown("joystick button 2"))
        {
            SceneTransition();
        }
        if (Input.GetKeyDown("joystick button 3"))
        {
            SceneTransition();
        }
        if (Input.GetKeyDown("joystick button 4"))
        {
            SceneTransition();
        }
        if (Input.GetKeyDown("joystick button 5"))
        {
            SceneTransition();
        }
        if (Input.GetKeyDown("joystick button 6"))
        {
            SceneTransition();
        }
        if (Input.GetKeyDown("joystick button 7"))
        {
            SceneTransition();
        }
        if (Input.GetKeyDown("joystick button 8"))
        {
            SceneTransition();
        }
        if (Input.GetKeyDown("joystick button 9"))
        {
            SceneTransition();
        }
        if (Input.GetKeyDown("joystick button 10"))
        {
            SceneTransition();
        }
        if (Input.GetKeyDown("joystick button 11"))
        {
            SceneTransition();
        }
    }
}
