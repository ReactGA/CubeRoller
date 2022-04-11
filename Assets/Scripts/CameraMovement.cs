using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform CubePlayer;
    bool islerping;
    [SerializeField] int CamThreshold = 3, offset = 2;
    [SerializeField] float lerpspeed = 0.4f;
    [SerializeField] bool ShowDebugVisual;
    GameObject CamFollower;
    Vector3 newPos;
    private void Start()
    {
        CamFollower = CreateFollwer();
    }
    GameObject CreateFollwer()
    {
        var obj = new GameObject("Camfollower");
        SetCamfollowPos(obj);
        return obj;
    }
    void SetCamfollowPos(GameObject obj)
    {
        var cubeXPos = Mathf.Round(CubePlayer.position.x);
        obj.transform.position = new Vector3
        (cubeXPos, Camera.main.transform.position.y, CubePlayer.position.z);
    }
    float xdiff()
    {
        return Mathf.Abs(Camera.main.transform.position.x - CamFollower.transform.position.x);
    }
    float dotProduct()
    {
        return Vector3.Dot(Camera.main.transform.forward,
        (CamFollower.transform.position - Camera.main.transform.position).normalized);
    }
    void Update()
    {
        DebugVisual();
        SetCamfollowPos(CamFollower);

        if (xdiff() >= CamThreshold /* && !islerping */)
        {
            newPos = new Vector3(CamFollower.transform.position.x + offset, Camera.main.transform.position.y, Camera.main.transform.position.z);
            islerping = true;
        }

        if (islerping)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
            newPos, lerpspeed);

            if (Camera.main.transform.position == newPos)
            {
                islerping = false;
            }
        }
    }
    void DebugVisual()
    {
        if (ShowDebugVisual)
        {
            Debug.Log(xdiff());
            Debug.DrawLine(Camera.main.transform.position,
            new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, CamFollower.transform.position.z), Color.red);
            Debug.DrawLine(Camera.main.transform.position,
            CamFollower.transform.position, Color.red);

        }
    }
}
