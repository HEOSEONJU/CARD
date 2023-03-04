using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : PlayerController
{

 

    private void Start()
    {

        m_CapsulleCollider  = this.transform.GetComponent<CapsuleCollider2D>();
        m_Anim = this.transform.Find("model").GetComponent<Animator>();
        m_rigidbody = this.transform.GetComponent<Rigidbody2D>();
  

    }



    private void Update()
    {



        checkInput();

        if (m_rigidbody.velocity.magnitude > 30)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x - 0.1f, m_rigidbody.velocity.y - 0.1f);

        }



    }

    public void checkInput()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_Anim.Play("Demo_Skill_1");

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_Anim.Play("Demo_Skill_2");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_Anim.Play("Demo_Skill_3");

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
      

            switch (Demo_GM.Gm.CharacterID)
            {
                case 0:
                    m_Anim.Play("Demo_Type1_Die");
                    break;
                case 1:
                    m_Anim.Play("Demo_Type2_Die");
                    break;
                case 2:
                    m_Anim.Play("Demo_Type3_Die");
                    break;
            }

        }

      




            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Skill_1"))
        {
           
            if (Is_Skill_1_Attack)
            {
               
                transform.transform.Translate(new Vector3(-transform.localScale.x * 25f * Time.deltaTime, 0, 0));

            }
            else
            {
                if (m_MoveX < 0)
                {

                    if (transform.localScale.x > 0)
                        transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));

                }

                else if (m_MoveX > 0)
                {

                    if (transform.localScale.x < 0)
                        transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));

                }
            }


        }





        if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Skill_1") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Skill_2")
            || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Skill_3"))
        {
          
            return;
        }



        if (Input.GetKey(KeyCode.Mouse1))
        {
            m_Anim.Play("Demo_Guard");
            return;
        }


        if (Input.GetKeyDown(KeyCode.S))  //아래 버튼 눌렀을때. 
        {

            IsSit = true;
            m_Anim.Play("Demo_Sit");


        }
        else if (Input.GetKeyUp(KeyCode.S))
        {

            m_Anim.Play("Demo_Idle");
            IsSit = false;

        }


        // sit나 die일때 애니메이션이 돌때는 다른 애니메이션이 되지 않게 한다. 
        if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Sit") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Type1_Die")|| m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Type2_Die")|| m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Type3_Die"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentJumpCount < JumpCount)  // 0 , 1
                {
                    DownJump();
                }
            }

            return;
        }


        m_MoveX = Input.GetAxis("Horizontal");


   
        GroundCheckUpdate();


        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {


                m_Anim.Play("Demo_Attack");
            }
            else
            {

                if (m_MoveX == 0)
                {
                    if (!OnceJumpRayCheck)
                        m_Anim.Play("Demo_Idle");

                }
                else
                {

                    m_Anim.Play("Demo_Run");
                }

            }
        }


       

     

        // 기타 이동 인풋.

        if (Input.GetKey(KeyCode.D))
        {

            if (isGrounded)  // 땅바닥에 있었을때. 
            {



                if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
                    return;

                transform.transform.Translate(Vector2.right* m_MoveX * MoveSpeed * Time.deltaTime);



            }
            else
            {

                transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));

            }




            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
                return;

            if (!Input.GetKey(KeyCode.A))
                Filp(false);


        }
        else if (Input.GetKey(KeyCode.A))
        {


            if (isGrounded)  // 땅바닥에 있었을때. 
            {



                if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
                    return;


                transform.transform.Translate(Vector2.right * m_MoveX * MoveSpeed * Time.deltaTime);

            }
            else
            {

                transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));

            }


            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
                return;

            if (!Input.GetKey(KeyCode.D))
                Filp(true);


        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
                return;


            if (currentJumpCount < JumpCount)  // 0 , 1
            {

                if (!IsSit)
                {
                    prefromJump();


                }
                else
                {
                    DownJump();

                }

            }


        }



    }


  


    protected override void LandingEvent()
    {


        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Run") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Demo_Attack"))
            m_Anim.Play("Demo_Idle");

    }

    public GameObject Skill1Prefab;
    public GameObject Skill2Prefab;
    public GameObject Skill3Prefab;

    public override void SkillAttack_Anim_1_Enter()
    {
        Is_Skill_1_Attack = true;


        GameObject tmpobj = Instantiate(Skill1Prefab, transform.position, Quaternion.identity);
        tmpobj.transform.localScale = new Vector3(-1 * transform.localScale.x, 1, 1);
        tmpobj.transform.SetParent(this.transform);
        tmpobj.transform.localPosition = new Vector3(-1.37f, 0.179f, 1);


    }
    public override void SkillAttack_Anim_1_Exit()
    {

        Is_Skill_1_Attack = false;


    }
    public override void SkillAttack_Anim_2_Enter()
    {

        GameObject tmpobj = Instantiate(Skill2Prefab, transform.position, Quaternion.identity);

        tmpobj.transform.SetParent(this.transform);
    }

   

 
    public override void SkillAttack_Anim_3_Enter()
    {
     

        GameObject tmpobj = Instantiate(Skill3Prefab, transform.position, Quaternion.identity);
        tmpobj.transform.position = new Vector2(this.transform.position.x- (transform.localScale.x*1.5f), this.transform.position.y-0.2f);
        Vector3 tmpDir = transform.localScale.x * this.transform.right;
        tmpobj.GetComponent<Skill_3>().Fire(tmpDir,20);
    
    }


    public override void Anim_Die_Enter()
    {
        Instantiate(BloodPrefab,this.transform.localPosition,Quaternion.identity)
            ;
    }


    public override void DefaulAttack_Collider(GameObject obj)
    {

        //Debug.Log("Attack11" + obj);
        if (obj.CompareTag("Monster"))
        {
            //    Debug.Log("Attack22"+ obj);
            Vector2 Knockdir = obj.transform.position - this.transform.position;
            obj.transform.root.GetComponent<Mon_Bass>().Damaged(10, Knockdir.normalized * 1.5f, 0.2f);
        }


    }


    public override void Skill_1Attack_Collider(GameObject obj)
    {

        if (obj.CompareTag("Monster"))
        {

           
        
            
              //  ISSKill1 = true;
                Vector2 Knockdir = obj.transform.position - this.transform.position;
                float StunTime = 1;

               
                obj.transform.parent.GetComponent<Mon_Bass>().Damaged(15, Knockdir.normalized * 0, StunTime);

                StartCoroutine(Skill_1Co(obj));
          
        }


    }

   
    IEnumerator Skill_1Co(GameObject obj)
    {
       
        obj.transform.parent.SetParent(this.transform);
        while (true)
        {

            if (!Is_Skill_1_Attack)
            {
                obj.transform.parent.SetParent(null);
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }




    }


    public override void Skill_2Attack_Collider(GameObject obj)
    {
        if (obj.CompareTag("Monster"))
        {

            Vector3 tmpos = new Vector3(this.transform.position.x - (this.transform.localScale.x * 1f), this.transform.position.y, this.transform.position.z);
            Vector2 Knockdir = obj.transform.position - tmpos;


            obj.transform.parent.GetComponent<Mon_Bass>().Damaged(15, Knockdir.normalized * 0, 1);

            StartCoroutine(Skill_2Co(obj.transform.parent.gameObject, -Knockdir * 3 + Vector2.up * 1));

        }


    }

    IEnumerator Skill_2Co(GameObject obj, Vector3 pos)
    {
        float tic = 0;
        float time = 1;
        while (true)
        {

            tic += 0.1f;
            obj.transform.Translate(pos * 1.5f * Time.deltaTime);
            if (tic > time)
            {
                tic = 0;

                //   transform.transform.Translate(new Vector3(-transform.localScale.x * 25f * Time.deltaTime, 0, 0));
                break;
            }


            yield return new WaitForSeconds(0.01f);
        }




    }



    public override void Skill_3Attack_Collider(GameObject obj)
    {




    }

    public override void Skill_4Attack_Collider(GameObject obj)
    {
    }







}
