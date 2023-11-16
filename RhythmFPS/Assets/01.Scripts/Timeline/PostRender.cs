using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostRender : MonoBehaviour
{
    public Material postMat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, postMat);
    }
}
