using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Clear_Log : MonoBehaviour
{
    [SerializeField]
    private Text ScoreText;

    private void Start()
    {
        ScoreText = GetComponent<Text>();
    }
    void Update()
    {
        if (!GameDirector.Instance.IsOpenRightDoor) return;

        if (GameDirector.Instance.IsOpenRightDoor)
        {
            ScoreText.text = ("ÉQÅ[ÉÄÉNÉäÉA!!");
        }
    }
}
