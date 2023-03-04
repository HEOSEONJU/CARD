using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionOfImage : MonoBehaviour
{
    [SerializeField]
    private Material m_effMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, m_effMaterial);
    }

}
