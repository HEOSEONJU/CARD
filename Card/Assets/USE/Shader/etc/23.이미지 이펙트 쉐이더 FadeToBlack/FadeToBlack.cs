using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField]
    Material m_effectMat;

    [SerializeField, Range(0, 1)]
    private float m_fadeAmount = 0;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        if (m_effectMat == null)
            return;

        Debug.Log("정상 출력");
        // 머테리얼에 연결된 셰이더에 _Fade 란 변수에 m_fadeAmount값을 전달합니다.
        m_effectMat.SetFloat("_Fade", m_fadeAmount);


        Graphics.Blit(source, destination, m_effectMat);

    }


}
