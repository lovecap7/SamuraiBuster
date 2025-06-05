using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryOrBuck : MonoBehaviour
{
    // �g��E�k���̑��x
    private float scaleSpeed = 0.5f;
    // �ŏ��X�P�[��
    private float minScale = 0.9f;
    // �ő�X�P�[��
    private float maxScale = 1.2f;

    private bool scalingUp = true;

    //�g��k�����邩���t���O�ŊǗ�
    private bool m_isActive = false;

    void Update()
    {
        if(m_isActive)
        {
            // ���݂̃X�P�[�����擾
            Vector3 currentScale = transform.localScale;

            // �g��E�k���̕����𔻒�
            if (scalingUp)
            {
                currentScale += Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale.x >= maxScale)
                {
                    currentScale = Vector3.one * maxScale;
                    scalingUp = false;
                }
            }
            else
            {
                currentScale -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (currentScale.x <= minScale)
                {
                    currentScale = Vector3.one * minScale;
                    scalingUp = true;
                }
            }

            // �X�P�[����K�p
            transform.localScale = currentScale;
        }
        else
        {
            //�ʏ�X�P�[����K�p
            transform.localScale = Vector3.one;
        }
    }

    public void SetIsActive(bool active)
    {
        m_isActive = active;
    }
    public bool GetIsActive()
    {
        return m_isActive;
    }
}
