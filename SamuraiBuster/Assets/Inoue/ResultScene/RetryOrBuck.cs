using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryOrBuck : MonoBehaviour
{
    // �g��E�k���̑��x
    [SerializeField] private float scaleSpeed;
    // �ŏ��X�P�[��
    [SerializeField] private float minScale;
    // �ő�X�P�[��
    [SerializeField] private float maxScale;

    private bool scalingUp = true;

    void Update()
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
}
