using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestRota : MonoBehaviour
{
    [SerializeField] private float m_speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector3(0.0f,0.0f,0.0f),Vector3.up,360.0f/ (1.0f / m_speed) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
