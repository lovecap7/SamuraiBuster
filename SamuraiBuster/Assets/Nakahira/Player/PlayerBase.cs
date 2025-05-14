using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    private Rigidbody m_rigid;
    const float kMoveSpeed = 10.0f;
    const float kDrag = 0.05f;
    Vector2 m_inputAxis = new();
    const float kRotateSpeed = 0.02f;
    Vector3 m_myVel = new();
    const float kRotateThreshold = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var keyBoard = Keyboard.current;

        // ƒ‹[ƒg2ˆÚ“®‚ð‘jŽ~
        m_inputAxis.Normalize();

        // ‰Á‘¬
        m_rigid.AddForce(new Vector3(m_inputAxis.x, 0, m_inputAxis.y) *kMoveSpeed);

        // X,Y‚¾‚¯Œ¸Š
        Vector3 tempVel = m_rigid.velocity;
        tempVel.x -= tempVel.x * kDrag;
        tempVel.z -= tempVel.z * kDrag;
        m_rigid.velocity = tempVel;

        m_myVel = tempVel;

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
