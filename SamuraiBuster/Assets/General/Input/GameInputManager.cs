using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    [SerializeField] private GameObject m_inputPrefab;

    // �����炩��͂ǂ�ȃC���X�^���X�����邩������Ȃ��̂ŁA�O�������Ă��炤
    List<IInputReceiver> m_receivers = new();
    List<GameInputHolder> m_inputHolders = new();

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;

        // Input�֘A�������Ȃ��悤�ɂ���
        // ���ꂪ�Q�[����ʂ��đ��݂��邱�ƂŁA�f�o�C�X���V���b�t�������̂�h��
        DontDestroyOnLoad(gameObject);

        // �v���C���[�̐l�������͋@�\�𐶐�
        int playerNum = PlayerPrefs.GetInt("PlayerNum");
        var pad = Gamepad.all;
        int padCount = pad.Count;
        for (int i = 0; i < playerNum; ++i)
        {
            Gamepad gamepad;

            // �Ⴆ�Ύl�l�v���C��I������3�䂵���R���g���[�����Ȃ����Ă��Ȃ����
            if (i > padCount - 1) // ���v�f���ƃC���f�b�N�X�����낦�Ă���
            {
                // ���̕��̃R���g���[�����蓖�Ă͕ۗ�
                // �����͂������̂ŁAnull�����Ƃ�
                gamepad = null;
            }
            else
            {
                gamepad = pad[i];
            }

            var instance = PlayerInput.Instantiate(m_inputPrefab, i, "Game", -1, gamepad);
            instance.transform.SetParent(transform, false);
        }

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

    public void AddReceiver(IInputReceiver receiver)
    {
        m_receivers.Add(receiver);
    }
}
