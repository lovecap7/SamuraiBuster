using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RolePlayerIsNum : MonoBehaviour
{
    private List<RoleSelectController> roleSelects;

    [SerializeField] private FadeManager m_fadeManager;
    private void Start()
    {
        // �v���C���[�̐l����c��
        roleSelects = new List<RoleSelectController>();
        int playerNum = PlayerPrefs.GetInt("PlayerNum");
        for (int i = 0; i < playerNum; ++i)
        {
            roleSelects.Add(transform.GetChild(i).GetComponent<RoleSelectController>());
        }
    }

    private void Update()
    {
        if(m_fadeManager.m_fadeAlpha >= 1.0f&& m_fadeManager.m_isFadeFinish)
        {
            ChangeScene();
        }
        if (m_fadeManager.m_isFadeOut) return;
        bool allDicided = true;
        if (roleSelects.Count == 0)
        {
            //Debug.Log("�v���C���[��0�l�ł�");
            return;
        }
        // �S�����I�����Ă��邩�`�F�b�N
        foreach (var role in roleSelects)
        {
            allDicided &= role.IsDecided();
        }

        if (!allDicided) return;
        m_fadeManager.m_isFadeOut = true;
    }

    private void ChangeScene()
    {
        int playerId = 0;

        // ���ꂼ�ꂪ���I��ł����E���L��
        foreach (var role in roleSelects)
        {
            string saveString = "PlayerRole" + playerId.ToString();
            PlayerPrefs.SetInt(saveString, (int)role.selectedRole);
            ++playerId;
        }

        // �v���C���[�𐶐�
        GameObject.Find("Players").GetComponent<PlayerGenerator>().GeneratePlayer();

        // �s���Ă�����Ⴂ
        // ��̃V�[���őI��ł����V�[���֔��
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage" + PlayerPrefs.GetInt("StageNum").ToString() + "Scene");
    }
}
