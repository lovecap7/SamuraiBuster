using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //Wave1
    [SerializeField] private Wave m_wave1;
    //Wave2
    [SerializeField] private Wave m_wave2;
    //Wave3
    [SerializeField] private Wave m_wave3;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        //Wave1Ç™èIÇÌÇ¡ÇΩÇ»ÇÁ
        if(m_wave1.GetIsWaveEnd())
        {
            Debug.Log("Wave1èIóπ");
        }
    }
}
