using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EmissiveOnOff : MonoBehaviour
{
    Material m_effmat;
    bool showEmissive = false;

    // 아래의 이벤트를 사용하기 위해서는 컬라이더가 있어야 동작합니다.
    private void OnMouseDown()
    {
        showEmissive = !showEmissive;

        if (showEmissive)
            m_effmat.SetFloat("_ShowEmissive", 1);
        
        else
            m_effmat.SetFloat("_ShowEmissive", 0);

    }
    // Start is called before the first frame update
    void Start()
    {
        m_effmat = GetComponent<Renderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
