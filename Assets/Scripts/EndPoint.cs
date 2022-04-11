using UnityEngine;

public class EndPoint : MonoBehaviour
{
    bool done;
    void OnEnable() {
        LevelManager.instance.EndPoint = transform;
    }
    void Start() {
        
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("childblocks") && !done)
        {
            LevelManager.instance.finishedSound.Play();
            LevelManager.instance.FinishedLevel();
            Invoke("ResetDone", 5f);
            done = true;
        }
    }
    void ResetDone()
    {
        done = false;
    }
}
