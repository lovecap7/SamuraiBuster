using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Clear_Log : MonoBehaviour
{
    private Text ScoreText;
    private void Start()
    {
        ScoreText = GetComponent<Text>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ScoreText.text = ("�Q�[���N���A!!");
            Debug.Log("�Q�[���N���A(C�L�[)��������܂����B");
        }
    }
}
