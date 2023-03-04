using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3 : MonoBehaviour
{

    public float movingSpeed = 10f;

    public float destroyTime = 2f;
    public float Damage = 0;

 
 
    public void Fire(Vector3 dir, float damamge)
    {
      
        this.transform.right = dir;
        Damage = damamge;
        StartCoroutine(destroyObj());

    }

  

   
    IEnumerator destroyObj()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);

    }

    Collider2D[] colliderpoint = new Collider2D[1];
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {

            Vector3 tmpos = new Vector3(this.transform.position.x - (this.transform.localScale.x * 1f), this.transform.position.y, this.transform.position.z);
            Vector2 Knockdir = other.transform.position - tmpos;


            other.transform.parent.GetComponent<Mon_Bass>().Damaged(Damage, Knockdir.normalized * 0, 1);

        }




    }



}
