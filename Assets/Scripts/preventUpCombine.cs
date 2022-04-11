using UnityEngine;

public class preventUpCombine : MonoBehaviour
{
    void FixedUpdate()
    {
        PreventCombine();
        if (RayVariables.inst.ShowDebugVisual)
        {
            DebugVisual();
        }
    }
    void PreventCombine()
    {
        if (preventRayhit().collider)
        {
            CubeRotate.instance.ResetPos();
            Debug.Log("Preventing...");
        }
    }
    RaycastHit preventRayhit()
    {
        var ray = new Ray(transform.position, Vector3.up);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, RayVariables.inst.rayDist, RayVariables.inst.mask);
        return hit;
    }

    void DebugVisual()
    {
        var ray = new Ray(transform.position, Vector3.up);
        Debug.DrawRay(ray.origin, ray.direction * RayVariables.inst.rayDist, Color.blue);
    }
}
