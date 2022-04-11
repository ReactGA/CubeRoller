using UnityEngine;

public class ReturnZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            /* c.GetComponent<Rigidbody>().velocity = Vector3.zero;
            c.transform.rotation = Quaternion.identity;
            c.transform.position = LevelManager.instance.startPoint.localPosition; */
            LevelManager.instance.RestartLevel();
        }
    }
}
