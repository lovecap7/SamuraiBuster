using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // ��������
    private float TotalScore;

    // �_���[�W�X�R�A
    [SerializeField]
    private int Damage;

    // ���j�X�R�A
    [SerializeField]
    private int Des;

    // �O��Update���̕b��
    private float oldSecTime;
    private Text ScoreText;
    private void Start()
    {
        ScoreText = GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TotalScore = TotalScore + Damage;
            Debug.Log("���{�^����������Ă��܂��B");
        }
        if (Input.GetMouseButtonDown(1))
        {
            TotalScore = TotalScore + Des;
            Debug.Log("�E�{�^����������Ă��܂��B");
        }
        ScoreText.text = TotalScore.ToString("0");
    }
}
