using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class GameInputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Input�֘A�������Ȃ��悤�ɂ���
        // ���ꂪ�Q�[����ʂ��đ��݂��邱�ƂŁA�f�o�C�X���V���b�t�������̂�h��
        DontDestroyOnLoad(gameObject);
    }
}
