using UnityEngine;

public class myPostProcesing : MonoBehaviour
{
    [SerializeField]Material PPMat;
    public void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src,dest,PPMat);
    }
}
