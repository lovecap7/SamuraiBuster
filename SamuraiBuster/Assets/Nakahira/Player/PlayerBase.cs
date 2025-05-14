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
    const float kInputAxisThreshold = 0.1f;
    const float kRotateSpeed = 0.2f;
    Vector3 m_myVel = new();

    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var keyBoard = Keyboard.current;

        // ƒ‹[ƒg2ˆÚ“®‚ğ‘j~
        m_inputAxis.Normalize();

        // ‰Á‘¬
        m_rigid.AddForce(new Vector3(m_inputAxis.x, 0, m_inputAxis.y) *kMoveSpeed);

        // X,Y‚¾‚¯Œ¸Š
        Vector3 tempVel = m_rigid.velocity;
        tempVel.x -= tempVel.x * kDrag;
        tempVel.z -= tempVel.z * kDrag;
        m_rigid.velocity = tempVel;

        m_myVel = tempVel;

        // ©•ª‚Å“®‚¢‚½ˆÚ“®•ûŒü‚ÉŒü‚«‚ğ•Ï‚¦‚é
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_myVel), kRotateSpeed);
    }

    public void GetMoveAxis(InputAction.CallbackContext context)
    {
        m_inputAxis = context.ReadValue<Vector2>();
    }
}
