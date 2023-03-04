using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spr = Resources.Load<SpriteRenderer>("GoblinKing");
        Material material = Resources.Load<Material>("CustomDissolve");
        SpriteRenderer left = Instantiate(spr, Vector3.left, Quaternion.identity);
        SpriteRenderer right = Instantiate(spr, Vector3.right, Quaternion.identity);

        left.material = Instantiate(material);
        right.material = Instantiate(material);

        Material imageMat = image.material;

        image.material = Instantiate(material);
        image.material.CopyPropertiesFromMaterial(imageMat);






    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
