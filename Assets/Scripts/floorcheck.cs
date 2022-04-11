using UnityEngine;

public class floorcheck : MonoBehaviour
{
    [HideInInspector] public bool floored;//, Notfloored, hasBegan, hasBegan1;
    void FixedUpdate()
    {
        DebugVisual();
        SetFloorHit();
    }
    #region Unused
    /* Debug.Log(transform.name + " collided with " + Rayhit().transform.name);
       Notfloored = false;
       if (hasBegan)
       {
           RayVariables.inst.cubeRotate.OnFloor();
           Debug.Log(transform.name + "  should be on floor, substracted one");
       }
       else
       {
           hasBegan = true;
       } */
    #endregion

    void SetFloorHit()
    {
        if (Rayhit().collider)
        {
            floored = true;
        }
        else
        {
            floored = false;
        }
    }
    Ray CreateRay(Vector3 startPoint)
    {
        Ray ray = new Ray(startPoint, -Vector3.up);
        return ray;
    }
    RaycastHit Rayhit()
    {
        RaycastHit ht;
        var ray = CreateRay(transform.position);
        Physics.Raycast(ray, out ht, RayVariables.inst.rayDist, RayVariables.inst.floormask);
        return ht;
    }
    void DebugVisual()
    {
        if (RayVariables.inst.ShowDebugVisual)
        {
            Ray ray = CreateRay(transform.position);
            Debug.DrawRay(ray.origin, ray.direction * RayVariables.inst.rayDist, Color.green);
        }
    }
}
