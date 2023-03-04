using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public Material dissolve;
    public Material newDissolve;
    private List<SpriteRenderer> spriteList = new List<SpriteRenderer>();
    void Start()
    {
        spriteList.AddRange(GetComponentsInChildren<SpriteRenderer>());

        newDissolve = Instantiate(dissolve);

        foreach (var sprite in spriteList)
        {
            // 디졸브 머테리얼의 복사본을 만들어서 저장합니다.
            sprite.material = newDissolve;
        }
    }

    IEnumerator IDissolve( float targetTime )
    {
        float elapsedTime = 0;
        newDissolve.SetFloat("_ShowDissolve", 1);
        while ( true )
        {
            elapsedTime += Time.deltaTime / targetTime;
            newDissolve.SetFloat("_DissolveAmount", elapsedTime);
            print(elapsedTime);
            if ( elapsedTime >= 1.0f)
            {
                break;
            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(IDissolve(1.0f));
        
    }
}
