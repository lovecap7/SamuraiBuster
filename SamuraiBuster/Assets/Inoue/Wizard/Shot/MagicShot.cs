using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShot : MonoBehaviour
{
    [SerializeField] private float m_liveTime = 10.0f;
   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        m_liveTime -= Time.deltaTime;
        if(m_liveTime <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //“G‚É“–‚½‚Á‚½‚çÁ‚¦‚é
        if (other.tag == "Fighter"  ||
            other.tag == "Mage"     ||
            other.tag == "Tank"     ||
            other.tag == "Healer"   ||
            other.tag == "Assassin")
        {
            Destroy(this.gameObject);
        }
    }
}
