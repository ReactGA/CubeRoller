using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class AnimateBlocks : MonoBehaviour
{
    [SerializeField] Transform BlocksHolder;

    // Update is called once per frame
    void Start()
    {
        Animate();
    }

    void Animate()
    {
        float leftmostPos = 0;
        int leftmostindex = 0;
        List<Transform> objs = new List<Transform>();
        List<float> xpos = new List<float>();
        for (var i = 0; i < BlocksHolder.childCount; i++)
        {
            var chld = BlocksHolder.GetChild(i);
            xpos.Add(chld.position.x);
            objs.Add(BlocksHolder.GetChild(i));
            if (chld.position.x < leftmostPos)
            {
                leftmostPos = chld.position.x;
                leftmostindex = i;
            }
        }
        Transform[] newobjs = new Transform[objs.Count];
        xpos.Sort();
        for (var i = 0; i < objs.Count; i++)
        {
            var chld = BlocksHolder.GetChild(i);
            for (var j = 0; j < xpos.Count; j++)
            {
                if (xpos[j] == chld.transform.position.x)
                {
                    newobjs[j] = chld;
                }
            }
        }
        foreach (Transform tr in newobjs)
        {
            Debug.Log($"pos is : {tr.transform.position.x} && name is {tr.name}");
        }

        //StartCoroutine(BeginAnimate(objs, leftmostindex));
    }

    IEnumerator BeginAnimate(List<Transform> objs, int index)
    {
        while (true)
        {
            for (var i = 0; i < objs.Count; i++)
            {
                if (objs[i].transform.localScale.x > 0)
                {
                    objs[i].transform.localScale -= Vector3.one * 0.1f;
                    yield return new WaitForSeconds(0.01f);
                }
                else
                {
                    StopAllCoroutines();
                }

            }
        }
        yield return null;
    }
}
