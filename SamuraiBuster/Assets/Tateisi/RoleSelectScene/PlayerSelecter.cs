using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelecter : MonoBehaviour
{
    Vector2 m_dirInput;
    bool m_isTrigger;
    int m_index;
    int m_beforeIndex;

    enum IconIndex
    {
        kFighter,
        kMage,
        kHealer,
        kTank,
        kMax
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_beforeIndex = m_index;

        if (m_dirInput.y >  0.1f)
        {
            m_index = (m_index + 1 + (int)IconIndex.kMax) % (int)IconIndex.kMax;
        }
        if (m_dirInput.y < -0.1f)
        {
            m_index = (m_index - 1 + (int)IconIndex.kMax) % (int)IconIndex.kMax;
        }

        if (m_beforeIndex == m_index || !m_isTrigger) return;

        // ƒAƒCƒRƒ“•Ï‚¦‚é
        transform.GetChild(m_beforeIndex).gameObject.SetActive(false);
        transform.GetChild(m_index).gameObject.SetActive(true);
    }

    public void InputDir(InputAction.CallbackContext context)
    {
        m_isTrigger = context.started;
        m_dirInput = context.ReadValue<Vector2>();
    }
}
