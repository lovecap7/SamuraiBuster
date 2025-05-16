using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    private Rigidbody m_rigid;
    const float kMoveSpeed = 1000.0f;
    Vector2 m_inputAxis = new();
    const float kRotateSpeed = 0.2f;
    Vector3 m_myVel = new();
    const float kRotateThreshold = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();

        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        // ƒ‹[ƒg2ˆÚ“®‚ð‘jŽ~
        m_inputAxis.Normalize();

        // ‰Á‘¬
        Vector3 addForce = kMoveSpeed * Time.deltaTime * new Vector3(m_inputAxis.x, 0, m_inputAxis.y);
        m_rigid.AddForce(addForce);

        m_myVel = addForce;

        // Ž©•ª‚Å“®‚¢‚½ˆÚ“®•ûŒü‚ÉŒü‚«‚ð•Ï‚¦‚é
        if (m_myVel.sqrMagnitude >= kRotateThreshold)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_myVel.normalized), kRotateSpeed);
        }
    }

    public void GetMoveAxis(InputAction.CallbackContext context)
    {
        m_inputAxis = context.ReadValue<Vector2>();
    }
}
