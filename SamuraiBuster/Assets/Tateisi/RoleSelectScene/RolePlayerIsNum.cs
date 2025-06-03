using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePlayerIsNum : MonoBehaviour
{
    private RoleSelectController roleSelect1;
    private RoleSelectController roleSelect2;
    private RoleSelectController roleSelect3;
    private RoleSelectController roleSelect4;

    private void Start()
    {
        roleSelect1 = transform.GetChild(0).GetComponent<RoleSelectController>();
        roleSelect2 = transform.GetChild(1).GetComponent<RoleSelectController>();
        roleSelect3 = transform.GetChild(2).GetComponent<RoleSelectController>();
        roleSelect4 = transform.GetChild(3).GetComponent<RoleSelectController>();
    }

    private void Update()
    {
        if (roleSelect1.IsDecided() && roleSelect2.IsDecided() && roleSelect3.IsDecided() && roleSelect4.IsDecided())
        {
            // 4l‘Sˆõ‚ª–ğŠ„‚ğŒˆ’è‚µ‚½ê‡‚Ìˆ—
            PlayerPrefs.SetInt("Player1Role", (int)roleSelect1.selectedRole);
            PlayerPrefs.SetInt("Player2Role", (int)roleSelect2.selectedRole);
            PlayerPrefs.SetInt("Player3Role", (int)roleSelect3.selectedRole);
            PlayerPrefs.SetInt("Player4Role", (int)roleSelect4.selectedRole);
            //Debug.Log("All Players Decided");
            UnityEngine.SceneManagement.SceneManager.LoadScene("StageScene");
        }
    }
}
