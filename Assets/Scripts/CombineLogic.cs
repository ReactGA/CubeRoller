using UnityEngine;
using System.Collections;

public class CombineLogic : MonoBehaviour
{
    [SerializeField] float Raylenght = 3;
    [SerializeField] LayerMask mask, floorMask;
    [SerializeField] Material cubeMat;
    [SerializeField] CubeRotate cubeRotate;
    [SerializeField] bool ShowDebugVisual = true;
    //bool floored, Notfloored;

    void FixedUpdate()
    {
        CastRays();
        if (ShowDebugVisual)
        {
            DebugVisual();
        }
    }
    void CastRays()
    {
        foreach (Transform child in transform)
        {
            CombineWith(hit(child.position), child);
        }
    }
    void CombineWith(RaycastHit[] hits, Transform ch)
    {

        for (var i = 0; i < 4; i++)
        {
            if (hits[i].collider)
            {
                StartCoroutine(Combine(hits[i].transform));
            }
        }
        #region Unused
        /* if (hits[4].collider && !floored)
   {
       Notfloored = false;
       cubeRotate.OnFloor();
       //Debug.Log(ch.name + "  should be on floor");
       floored = true;
       //make sure this is called only once
   }
   else if (!hits[4].collider && !Notfloored)
   {
       floored = false;
       cubeRotate.NotOnFloor();
       //Debug.Log(ch.name + "  go down");
       Notfloored = true;
       //make sure this is called only once
   } */
        #endregion

    }
    IEnumerator Combine(Transform hit)
    {
        var cubeToCombime = hit;
        Vector3 worldPos = cubeToCombime.position;
        cubeToCombime.gameObject.layer = 8;
        var rend = cubeToCombime.GetComponent<Renderer>();
        rend.material = cubeMat;
        var duplicate = Instantiate(cubeToCombime, cubeToCombime.position, cubeToCombime.rotation).gameObject;
        rend.enabled = false;
        cubeToCombime.SetParent(transform, false);
        cubeToCombime.gameObject.tag = "childblocks";
        Destroy(cubeToCombime.GetComponent<preventUpCombine>());
        cubeToCombime.gameObject.AddComponent<floorcheck>();
        cubeRotate.SetChecks();
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 0.5f);
        yield return null;
        cubeToCombime.transform.position = worldPos;
        rend.enabled = true;
        Destroy(duplicate);
        while (rend.material.color.a < 1)
        {
            rend.material.color += new Color(0, 0, 0, 0.05f);
            yield return null;
        }
    }
    RaycastHit[] hit(Vector3 childPos)
    {
        RaycastHit[] ht = new RaycastHit[4];
        CreateRayCast(ht, childPos);
        return ht;
    }
    void CreateRayCast(RaycastHit[] ht, Vector3 childPos)
    {
        var ray = CreateRay(childPos);
        Physics.Raycast(ray[0], out ht[0], Raylenght, mask);
        Physics.Raycast(ray[1], out ht[1], Raylenght, mask);
        Physics.Raycast(ray[2], out ht[2], Raylenght, mask);
        Physics.Raycast(ray[3], out ht[3], Raylenght, mask);
        //floor check
        //Physics.Raycast(ray[4], out ht[4], Raylenght, floorMask);
    }
    Ray[] CreateRay(Vector3 startPoint)
    {
        Ray[] ray = new Ray[4];
        ray[0] = new Ray(startPoint, Vector3.forward);
        ray[1] = new Ray(startPoint, -Vector3.forward);
        ray[2] = new Ray(startPoint, Vector3.left);
        ray[3] = new Ray(startPoint, -Vector3.left);
        //floor check
        //ray[4] = new Ray(startPoint, -Vector3.up);
        return ray;
    }
    void DebugVisual()
    {
        foreach (Transform child in transform)
        {
            var ray = CreateRay(child.position);
            Debug.DrawRay(ray[0].origin, ray[0].direction * Raylenght, Color.blue);
            Debug.DrawRay(ray[1].origin, ray[1].direction * Raylenght, Color.blue);
            Debug.DrawRay(ray[2].origin, ray[2].direction * Raylenght, Color.blue);
            Debug.DrawRay(ray[3].origin, ray[3].direction * Raylenght, Color.blue);
            //Debug.DrawRay(ray[4].origin, ray[4].direction * Raylenght, Color.red);
        }
    }
}
