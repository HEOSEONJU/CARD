using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChromaticAberration : MonoBehaviour
{
    [SerializeField]
    private Material m_effectMat;
    [SerializeField]
    private Vector2 m_aberrationR;
    [SerializeField]
    private Vector2 m_aberrationG;
    [SerializeField]
    private Vector2 m_aberrationB;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (m_effectMat == null)
            return;
        m_effectMat.SetVector("_AberrationR", m_aberrationR);
        m_effectMat.SetVector("_AberrationG", m_aberrationG);
        m_effectMat.SetVector("_AberrationB", m_aberrationB);

        Graphics.Blit(source, destination, m_effectMat);
    }



}
