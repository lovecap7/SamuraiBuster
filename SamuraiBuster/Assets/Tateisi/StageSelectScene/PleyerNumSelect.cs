using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PleyerNumSelect : MonoBehaviour
{
    public GameObject NumSelect;


    [SerializeField]
    private bool activeState;  // アクティブ状態
    [SerializeField]
    private bool isStage1;  // ステージ1が選択されているかどうか
    [SerializeField]
    private bool isStage2;  // ステージ2が選択されているかどうか
    [SerializeField]
    private bool isStage3;  // ステージ3が選択されているかどうか

    void Start()
    {
        this.NumSelect.SetActive(activeState);
    }


    void Update()
    {
        isStage1 = selectstage_1.Instance.Stage1;
        isStage2 = selectstage_2.Instance.Stage2;
        isStage3 = selectstage_3.Instance.Stage3;
        if (isStage1 || isStage2 || isStage3)
        {
            activeState = true;
        }
        else
        {
            activeState = false;
        }
        this.NumSelect.SetActive(activeState);
    }
}
