using UnityEngine;

public class FPS : MonoBehaviour
{
    public int FrameRate = 60;

    void Start()
    {
        Application.targetFrameRate = FrameRate;
    }
}
