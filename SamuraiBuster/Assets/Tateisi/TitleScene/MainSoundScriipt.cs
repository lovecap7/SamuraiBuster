using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSoundScriipt : MonoBehaviour
{
    // ���������ɑ��݂��Ă��邩���m�F���邽�߂̃t���O
    private static bool isInstanceExisnt = false;
    private void Awake()
    {
        if (isInstanceExisnt)
        { 
            // ���łɃ��[�h����Ă�����
            Destroy(this.gameObject); // �������g��j�����ďI��
            return;
        }
        // ���݂��邱�Ƃ��L�^
        isInstanceExisnt = true;
        // �V�[����J�ڂ��Ă��I�u�W�F�N�g��j�����Ȃ��悤�ɂ���
        DontDestroyOnLoad(this.gameObject);
    }

}
//using UnityEngine.SceneManagement;

//public class MainSoundScriipt : MonoBehaviour
//{
//    // �V�[���J�ڎ���BGM���Đ����邽�߂̃X�N���v�g
//    // ���̃X�N���v�g�́A
//    // �^�C�g���V�[��,�X�e�[�W�Z���N�g�V�[��,���[���Z���N�g�V�[��
//    // ��BGM�𗬂����߂Ɏg�p����܂��B

//    // �V���O���g���̐ݒ�

//    static public MainSoundScriipt insnancs;

//    private void Awake()
//    {
//        if (insnancs == null)
//        {
//            // �V���O���g���̃C���X�^���X�����݂��Ȃ��ꍇ
//            insnancs = this; // �C���X�^���X��ݒ�
//            DontDestroyOnLoad(this.gameObject); // �V�[���J�ڂ��Ă��I�u�W�F�N�g��j�����Ȃ��悤�ɂ���
//        }
//        else
//        {
//            // ���łɃV���O���g���̃C���X�^���X�����݂���ꍇ
//            Destroy(this.gameObject); // �������g��j�����ďI��
//        }
//    }
//    // �V���O���g���̐ݒ�I���





//    public AudioSource bgm; // AudioSource�^�̕ϐ�bgm��錾�@�Ή�����AudioSource�R���|�[�l���g���A�^�b�`

//    public string sceneName; // �V�[�������i�[����ϐ�

//    private void Start()
//    {
//        sceneName = "TitleScene"; // �����V�[������ݒ�
//        bgm.Play(); // BGM���Đ�

//        // �V�[�����ύX���ꂽ�Ƃ��ɌĂяo�����C�x���g��o�^
//        SceneManager.activeSceneChanged += OnActiveSceneChanged;
//    }

//    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
//    {
//        // �V�[�����ǂ��ς���������m�F

//        // TitleScene����StageSelectScene��
//        if (sceneName == "TitleScene" && nextScene.name == "SceneSelectScene")
//        {
//            sceneName = "SceneSelectScene"; // �V�[�������X�V
//            bgm.Play(); // BGM���Đ�
//        }

//        // StageSelectScene����RollSelectScene��
//        if (sceneName == "SceneSelectScene" && nextScene.name == "1PlayRollSelectScene")
//        {
//            sceneName = "1PlayRollSelectScene"; // �V�[�������X�V
//            bgm.Play(); // BGM���Đ�
//        }

//        // RoleSelectScene����StageScene��
//        if (sceneName == "1PlayRollSelectScene" && nextScene.name == "StageScene")
//        {
//            sceneName = "StageScene"; // �V�[�������X�V
//            bgm.Stop(); // BGM���~
//        }

//        //�J�ڌ�̃V�[�������u�P�O�̃V�[�����v�Ƃ��ĕێ�
//        sceneName = nextScene.name;

//    }
//}