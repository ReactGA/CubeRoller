using UnityEngine;

public class framehandler : MonoBehaviour
{
    [SerializeField] int frameRate = 60;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;
    }
}
