using UnityEngine;

public class Levelinstance : MonoBehaviour
{
    public static Levelinstance inst;
    [HideInInspector]public int currentLevel;
    public bool BeganGame;
    void Awake()
    {
        if (inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this);
        }
        else
        { 
            Destroy(this);
        }
    }
}
