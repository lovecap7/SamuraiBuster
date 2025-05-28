using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HitGateActive : MonoBehaviour
{
    public GameObject HitGateLeft;
    public GameObject HitGateRight;

    [SerializeField] private bool activeState;
    [SerializeField] private bool activeClear;


    void Start()
    {
        this.HitGateLeft.SetActive(activeState);
        this.HitGateRight.SetActive(activeClear);
    }

    private void Update()
    {
        StarteAct();
        ClearAct();
    }

    private void StarteAct()
    {
        if (GameDirector.Instance.IsOpenLeftDoor)
        {
            activeState = false;
        }
        if (!GameDirector.Instance.IsOpenLeftDoor)
        {
            activeState = true;
        }
        this.HitGateLeft.SetActive(activeState);
    }

    private void ClearAct()
    {
        if (GameDirector.Instance.IsOpenRightDoor)
        {
            activeClear = false;
        }
        if (!GameDirector.Instance.IsOpenRightDoor && !activeClear)
        {
            activeClear = true;
        }
        this.HitGateRight.SetActive(activeClear);
    }
}
