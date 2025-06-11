using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using StageKind;

public class LightColor : MonoBehaviour
{
    Light m_light;
    readonly Color kStage1Color = new(1.0f,0.95f,0.84f);
    readonly Color kStage2Color = new(0.36f,0.41f,1.0f);
    readonly Color kStage3Color = new(0.39f,0.08f,0.39f);
    readonly Vector3 kStage1Rotation = new(  10, 300, 0);
    readonly Vector3 kStage2Rotation = new(   5, 300, 0);
    readonly Vector3 kStage3Rotation = new(  -5, 300, 0);

    Color m_baseColor;
    Color m_targerColor;
    Vector3 m_baseRotation;
    Vector3 m_targetRotation;
    const float kLerpSpeed = 0.03f;
    float m_timer = 0;
    StageKindEnum m_nowStageKind = StageKindEnum.Stage1;

    // Start is called before the first frame update
    void Start()
    {
        m_light = GetComponent<Light>();
        m_baseColor = kStage1Color;
        m_targerColor = kStage1Color;
        m_baseRotation = kStage1Rotation;
        m_targetRotation = kStage1Rotation;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer += kLerpSpeed;

        if (m_timer >= 1.0f)
        {
            m_timer = 1.0f;
        }

        // 今のカラーをターゲットに線形補間
        m_light.color = Color.Lerp(m_baseColor, m_targerColor, m_timer);
        // 透明度は1
        m_light.color += new Color(0,0,0,1);

        transform.rotation = Quaternion.Lerp(Quaternion.Euler(m_baseRotation), Quaternion.Euler(m_targetRotation), m_timer);
    }

    public void ChangeLightColor(StageKindEnum nextKind)
    {
        m_timer = 0;

        switch (m_nowStageKind)
        {
            case StageKindEnum.Stage1:
                m_baseColor = kStage1Color;
                m_baseRotation = kStage1Rotation;
                break;
            case StageKindEnum.Stage2:
                m_baseColor = kStage2Color;
                m_baseRotation = kStage2Rotation;
                break;
            case StageKindEnum.Stage3:
                m_baseColor = kStage3Color;
                m_baseRotation = kStage3Rotation;
                break;
        }

        switch (nextKind)
        {
            case StageKindEnum.Stage1:
                m_targerColor = kStage1Color;
                m_targetRotation = kStage1Rotation;
                break;
            case StageKindEnum.Stage2:
                m_targerColor = kStage2Color;
                m_targetRotation = kStage2Rotation;
                break;
            case StageKindEnum.Stage3:
                m_targerColor = kStage3Color;
                m_targetRotation = kStage3Rotation;
                break;
        }

        m_nowStageKind = nextKind;
    }
}
