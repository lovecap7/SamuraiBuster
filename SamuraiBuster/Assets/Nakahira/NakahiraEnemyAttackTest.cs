using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NakahiraEnemyAttackTest : MonoBehaviour
{
    public int damage = 0;

    AttackPower attackPower;

    // Start is called before the first frame update
    void Start()
    {
        attackPower = GetComponent<AttackPower>();
    }

    // Update is called once per frame
    void Update()
    {
        attackPower.damage = damage;
    }
}
