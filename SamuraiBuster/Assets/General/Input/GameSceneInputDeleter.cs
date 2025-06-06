using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲームシーンに置いて、PlayerInputsがいらなくなったら消す
public class GameSceneInputDeleter : MonoBehaviour
{
    private void OnDestroy()
    {
        GameObject.Destroy(GameObject.Find("PlayerInputs"));   
    }
}
