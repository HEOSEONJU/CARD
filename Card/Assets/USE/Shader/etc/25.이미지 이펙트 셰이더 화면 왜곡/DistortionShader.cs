using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistortionShader : MonoBehaviour
{
    [SerializeField]
    private Material m_effMaterial;

    [Range(0, 1)]
    public float m_waveAmount = 0.13f;
    [Range(0, 10)]
    public float _XSpeed;
    [Range(0, 10)]
    public float _YSpeed;
    [Range(0, 10)]
    public float _xWiggly;
    [Range(0, 10)]
    public float _yWiggly;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        m_effMaterial.SetFloat("_waveAmount", m_waveAmount);
        m_effMaterial.SetFloat("_XSpeed", _XSpeed);
        m_effMaterial.SetFloat("_YSpeed", _YSpeed);
        m_effMaterial.SetFloat("_xWiggly", _xWiggly);
        m_effMaterial.SetFloat("_yWiggly", _yWiggly);

        Graphics.Blit(source, destination, m_effMaterial);
    }

}
