using UnityEngine;

public class RayVariables : MonoBehaviour
{
    public static RayVariables inst;
    public float rayDist = 1.1f;
    public bool ShowDebugVisual = true;
    public LayerMask mask;
    public LayerMask floormask;
    public CubeRotate cubeRotate;
    private void Awake()
    {
        inst = this;
    }
}
