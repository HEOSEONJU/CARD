using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertShader : MonoBehaviour
{
    [SerializeField]
    private Material m_effectMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // OnRenderImage 함수는 신이 모두 그려진 이후에 ( 모든 것이 렌더링된 이후 ) 호출되는 함수입니다.
    // source 현재 계산된 값 
    // destination 이미 출력된 값.
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        
        // 소스 텍스처를 타겟으로 하는 렌더 텍스쳐 안으로 복사하는 함수입니다.
        Graphics.Blit(source, destination, m_effectMaterial);
    }

}
