using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Q�[���V�[���ɒu���āAPlayerInputs������Ȃ��Ȃ��������
public class GameSceneInputDeleter : MonoBehaviour
{
    private void OnDestroy()
    {
        GameObject.Destroy(GameObject.Find("PlayerInputs"));   
    }
}
