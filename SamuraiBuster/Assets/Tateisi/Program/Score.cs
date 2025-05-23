using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // ���v�X�R�A
    private float TotalScore;

    // �O��Update���̕b��
    private float oldSecTime;
    // �X�R�A�\���pUI�e�L�X�g
    [SerializeField]
    private Text ScoreText;
    private void Start()
    {
        ScoreText = GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TotalScore = TotalScore + GameManager.Instance.Damage;
            Debug.Log("���{�^����������Ă��܂��B");
        }
        if (Input.GetMouseButtonDown(1))
        {
            TotalScore = TotalScore + GameManager.Instance.Des;
            Debug.Log("�E�{�^����������Ă��܂��B");
        }
        ScoreText.text = TotalScore.ToString("0");
    }
}
