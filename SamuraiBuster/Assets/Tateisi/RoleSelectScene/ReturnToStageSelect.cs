using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Collections.Generic;

public class ReturnToStageSelect : MonoBehaviour
{
    int m_pressTimer = 0;
    // ��b�߂�{�^������������߂�
    const int kReturnPressTime = 60;
    // �߂�{�^����������UI
    Image m_returnImage;
    // �L�����o�X�O���[�v�œ����x��ς���
    [SerializeField]
    CanvasGroup m_fadeCanvas;
    [SerializeField]
    Image m_fadeImage;
    bool m_isFading = false;

    private void Start()
    {
        m_returnImage = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isFading) return;

        // ���͂��Ƃ�
        // PlayerInput�g�킸�ɓK����InputSystem����q�؂���

        var pads = Gamepad.all;

        bool isPressedB = false;
        foreach (var pad in pads)
        {
            isPressedB |= pad.bButton.isPressed;
        }
        if (isPressedB)
        {
            ++m_pressTimer;
        }
        else
        {
            --m_pressTimer;
        }

        m_pressTimer = Mathf.Clamp(m_pressTimer, 0, kReturnPressTime);

        // �摜�𔽉f
        m_returnImage.fillAmount = (float)m_pressTimer / (float)kReturnPressTime;

        if (m_pressTimer >= kReturnPressTime)
        {
            ToStageSelect();
        }
    }

    void ToStageSelect()
    {
        m_isFading = true;

        // �v���C���[�A�C���v�b�g���폜
        // �V�[���J��

        Destroy(GameObject.Find("PlayerInputs"));
        Destroy(GameObject.Find("Players"));

        // �t�F�[�h�̃A���t�@��255�ɂ���
        m_fadeImage.color += new Color(0,0,0,1);
        // �L�����o�X�O���[�v�𓧖��ɂ���
        m_fadeCanvas.alpha = 0;

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while(true)
        {
            m_fadeCanvas.alpha += 1.0f / kReturnPressTime;

            if (m_fadeCanvas.alpha >= 1.0f)
            {
                SceneManager.LoadScene("StageSelectScene");
                yield break;
            }

            yield return null;
        }
    }
}
