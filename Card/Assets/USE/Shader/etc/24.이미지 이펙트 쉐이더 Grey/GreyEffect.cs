using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 에디트 모드에서 실행하고자 할 경우 추가해서 사용하시면 됩니다.
[ExecuteInEditMode]
public class GreyEffect : MonoBehaviour
{
    [SerializeField]
    private Material m_effectMat;

    [SerializeField, Range(0, 1)]
    private float m_greyAmount = 0.5f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (m_effectMat == null) return;

        m_effectMat.SetFloat("_greyAmount", m_greyAmount);
        Graphics.Blit(source, destination, m_effectMat);
    }


}
