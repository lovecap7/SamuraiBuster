using UnityEngine;

public class Fighter : PlayerBase
{
    // 剣のオブジェクト
    CapsuleCollider m_sword;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //m_sword = GameObject.Find("Sword").GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // 剣を振る
    public override void Attack()
    {
        // アニメーション
        //m_anim.SetBool("Attaking", true);

        // 剣の当たり判定をオンにする
        //m_sword.enabled = true;

        //Debug.Log("Attack!!");
    }
}
