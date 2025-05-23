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
        if (GameDirector.Instance.IsGameCleared)
        {
            ScoreText.text = ("ÉQÅ[ÉÄÉNÉäÉA!!");
        }
    }
}
