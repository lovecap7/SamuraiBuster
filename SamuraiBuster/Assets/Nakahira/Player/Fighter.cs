using UnityEngine;

public class Fighter : PlayerBase
{
    [SerializeField] GameObject m_katana;
    CapsuleCollider m_katanaCollider;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        m_katanaCollider = m_katana.GetComponent<CapsuleCollider>();
        m_katanaCollider.enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        //m_anim.SetBool("Attaking", true);
    }

    public void EnableKatanaCol()
    {
        m_katanaCollider.enabled = true;
    }

    public void DisableKatanaCol()
    {
        m_katanaCollider.enabled = false;
    }
}
