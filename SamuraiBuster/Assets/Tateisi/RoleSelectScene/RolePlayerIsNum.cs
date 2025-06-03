using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RolePlayerIsNum : MonoBehaviour
{
    private List<RoleSelectController> roleSelects;

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
        bool allDicided = false;
        foreach (var role in roleSelects)
        {
            allDicided |= role.IsDecided();
        }

        if (!allDicided) return;

        int playerId = 0;

        foreach (var role in roleSelects)
        {
            string saveString = "PlayerRole" + playerId.ToString();
            PlayerPrefs.SetInt(saveString, (int)role.selectedRole);
            ++playerId;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("StageScene");
    }
}
