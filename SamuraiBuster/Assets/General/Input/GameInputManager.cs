using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    // �����炩��͂ǂ�ȃC���X�^���X�����邩������Ȃ��̂ŁA�O�������Ă��炤
    public List<IInputReceiver> m_receivers = new();
    List<GameInputHolder> m_inputHolders = new();

    // Start is called before the first frame update
    void Start()
    {
        // Input�֘A�������Ȃ��悤�ɂ���
        // ���ꂪ�Q�[����ʂ��đ��݂��邱�ƂŁA�f�o�C�X���V���b�t�������̂�h��
        DontDestroyOnLoad(gameObject);

        // �������擾
        for (int i = 0; i < transform.childCount; ++i)
        {
            m_inputHolders.Add(transform.GetChild(i).GetComponent<GameInputHolder>());
        }

        SetInterface();
    }

    // �eGameInput�ɑΉ������C���^�[�t�F�[�X��n��
    void SetInterface()
    {
        // �����������݂���Ǝv������
        int id = 0;
        foreach (var holder  in m_inputHolders)
        {
            holder.receiver = m_receivers[id];
            ++id;
        }
    }
}
