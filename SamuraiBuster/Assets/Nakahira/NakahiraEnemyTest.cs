using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NakahiraEnemyTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerMeleeAttack") || other.CompareTag("PlayerRangeAttack"))
        {
            Debug.Log("���������I");
            Debug.Log($"����:{other.tag},�_���[�W:{other.GetComponent<AttackPower>().damage}");
        }
    }
}
