using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectVector : MonoBehaviour
{
    Vector2 LightVec = new Vector2(-0.7f, 0.7f);
    Vector2 normalVec = new Vector2(0.0f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        // 투영 벡터를 구합니다.
        Vector2 projection = Vector2.Dot(LightVec, normalVec) * normalVec;

        // 투영 벡터를 두배로 누적합니다.
        projection *= 2;

        // 투영 벡터에서 광원의 위치를 빼면 반사 벡터를 구할 수 있습니다.
        Vector2 reflect =  projection - LightVec;

        Debug.Log(reflect);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
