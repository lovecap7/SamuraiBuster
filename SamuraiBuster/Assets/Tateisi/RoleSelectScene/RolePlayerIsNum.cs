using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePlayerIsNum : MonoBehaviour
{
    [SerializeField] private RoleSelect_1 roleSelect1;
    [SerializeField] private RoleSelect_2 roleSelect2;
    [SerializeField] private RoleSelect_3 roleSelect3;
    [SerializeField] private RoleSelect_4 roleSelect4;

    private void Update()
    {
        if (roleSelect1.IsDecided() && roleSelect2.IsDecided() && roleSelect3.IsDecided() && roleSelect4.IsDecided())
        {
            // 4l‘Sˆõ‚ª–ğŠ„‚ğŒˆ’è‚µ‚½ê‡‚Ìˆ—
            PlayerPrefs.SetInt("Player1Role", (int)roleSelect1.SelectedRole);
            PlayerPrefs.SetInt("Player2Role", (int)roleSelect2.SelectedRole);
            PlayerPrefs.SetInt("Player3Role", (int)roleSelect3.SelectedRole);
            PlayerPrefs.SetInt("Player4Role", (int)roleSelect4.SelectedRole);
            //Debug.Log("All Players Decided");
            UnityEngine.SceneManagement.SceneManager.LoadScene("StageScene");
        }
    }
}
