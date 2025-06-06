using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RoleSelectController : MonoBehaviour, IInputReceiver
{
    private Tweener m_punch;
    private Vector3 initPos;
    private GameObject m_upArrow;
    private GameObject m_downArrow;
    private GameObject m_activeStandModel;

    // この順番でヒエラルキーに置いてある前提
    public enum RoleKind:int
    {
        Fighter,
        Healer,
        Mage,
        Tank,
        Max
    }

    // 選択されたロール
    public RoleKind selectedRole {  get; private set; }

    // プレイヤーがまとまっているオブジェクト
    [SerializeField]
    GameObject m_players; 

    private bool isDecided = false; // 決定済みフラグ
    public bool IsDecided()
    {
        return isDecided;
    }

    // 初期化処理
    void Start()
    {
        selectedRole = RoleKind.Fighter; // 初期ロールを設定
        m_activeStandModel = m_players.transform.GetChild((int)selectedRole).gameObject;
        m_activeStandModel.SetActive(true);
        transform.GetChild((int)selectedRole).gameObject.SetActive(true);

        // 自分が上から何番目かで紐づけるPlayerInputを決める
        GameInputManager gameInputManager = GameObject.Find("PlayerInputs").GetComponent<GameInputManager>();
        gameInputManager.AddReceiver(this);

        initPos = transform.localPosition;

        m_upArrow = transform.GetChild(4).gameObject;
        m_downArrow = transform.GetChild(5).gameObject;
    }

    // 更新処理
    private void Update()
    {
    }

    /// <summary>
    /// ロール選択の上入力時の動き
    /// </summary>
    private void Up()
    {
        m_punch?.Kill();
        transform.localPosition = initPos;

        RoleKind before = selectedRole;
        selectedRole = (RoleKind)(((int)selectedRole - 1 + (int)RoleKind.Max) % (int)RoleKind.Max);
        m_punch = transform.DOPunchPosition(new Vector3(0,10,0), 0.2f);
        ChangeIcon(before, selectedRole);
        ChangeModel(before, selectedRole);
    }

    /// <summary>
    /// ロール選択の下入力時の動き
    /// </summary>
    private void Down()
    {
        m_punch?.Kill();
        transform.localPosition = initPos;

        RoleKind before = selectedRole;
        selectedRole = (RoleKind)(((int)selectedRole + 1 + (int)RoleKind.Max) % (int)RoleKind.Max);
        m_punch = transform.DOPunchPosition(new Vector3(0,-10,0), 0.2f);
        ChangeIcon(before, selectedRole);
        ChangeModel(before, selectedRole);
    }

    private void ChangeIcon(RoleKind beforeRole, RoleKind nextRole)
    {
        // 今までのアイコンを消す
        transform.GetChild((int)beforeRole).gameObject.SetActive(false);
        // 次をつける
        transform.GetChild((int)nextRole).gameObject.SetActive(true);
    }

    private void ChangeModel(RoleKind beforeRole, RoleKind nextRole)
    {
        m_activeStandModel.SetActive(false);
        m_activeStandModel = m_players.transform.GetChild((int)nextRole).gameObject;
        m_activeStandModel.SetActive(true);
    }

    public void Submit()
    {
        isDecided = true; // 決定済みフラグを立てる

        transform.DOPunchScale(Vector3.one, 0.2f);
        m_upArrow.GetComponent<Image>().color = Color.gray;
        m_downArrow.GetComponent<Image>().color = Color.gray;
        // プレイヤーにエモートさせる
        m_activeStandModel.GetComponent<Animator>().SetTrigger("Emote");
    }

    public void Cancel()
    {
        isDecided = false; // 決定をキャンセルする
        m_upArrow.GetComponent<Image>().color = Color.white;
        m_downArrow.GetComponent<Image>().color = Color.white;
        // プレイヤーにエモートさせる
        // 同じトリガーでキャンセルもさせてます
        m_activeStandModel.GetComponent<Animator>().SetTrigger("Emote");
    }

    public void TriggerUp()
    {
        if (isDecided) return; // 決定済みの場合は何もしない
        Up();
    }

    public void TriggerDown()
    {
        if (isDecided) return; // 決定済みの場合は何もしない

        Down();
    }

    public void TriggerRight()
    {
        return;
    }

    public void TriggerLeft()
    {
        return;
    }

    public void Attack()
    {
        return;
    }

    public void Skill()
    {
        return;
    }

    public void Move(Vector2 axis)
    {
        // 何もしない
        return;
    }
}
