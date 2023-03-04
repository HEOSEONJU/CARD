using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController :MonoBehaviour
{
    public bool IsSit = false;
    public int currentJumpCount = 0; 
    public bool isGrounded = false;
    public bool OnceJumpRayCheck = false;

    public bool Is_DownJump_GroundCheck = false;   // 다운 점프를 하는데 아래 블록인지 그라운드인지 알려주는 불값
    protected float m_MoveX;
    public Rigidbody2D m_rigidbody;
    protected CapsuleCollider2D m_CapsulleCollider;
    protected Animator m_Anim;

    [Header("[Setting]")]
    public float MoveSpeed = 6;
    public int JumpCount = 2;
    public float jumpForce = 15f;

    public GameObject BloodPrefab;

    public float Damage;


    protected void Filp(bool bLeft)
    {


        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);

    }


    protected void prefromJump()
    {
        m_Anim.Play("Demo_Jump");

        m_rigidbody.velocity = new Vector2(0, 0);

        m_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        OnceJumpRayCheck = true;
        isGrounded = false;


        currentJumpCount++;

    }

    protected void DownJump()
    {
        if (!isGrounded)
            return;


        if (!Is_DownJump_GroundCheck)
        {
            m_Anim.Play("Demo_Jump");

            m_rigidbody.AddForce(-Vector2.up * 10);
            isGrounded = false;

            m_CapsulleCollider.enabled = false;

            StartCoroutine(GroundCapsulleColliderTimmerFuc());

        }


    }

    IEnumerator GroundCapsulleColliderTimmerFuc()
    {
        yield return new WaitForSeconds(0.3f);
        m_CapsulleCollider.enabled = true;
    }


    //////바닥 체크 레이케스트 
    Vector2 RayDir = Vector2.down;


    float PretmpY;
    float GroundCheckUpdateTic = 0;
    float GroundCheckUpdateTime = 0.01f;
    protected void GroundCheckUpdate()
    {
        if (!OnceJumpRayCheck)
            return;

        GroundCheckUpdateTic += Time.deltaTime;



        if (GroundCheckUpdateTic > GroundCheckUpdateTime)
        {
            GroundCheckUpdateTic = 0;



            if (PretmpY == 0)
            {
                PretmpY = transform.position.y;
                return;
            }



            float reY = transform.position.y - PretmpY;  //    -1  - 0 = -1 ,  -2 -   -1 = -3

            if (reY <= 0)
            {

                if (isGrounded)
                {

                    LandingEvent();
                    OnceJumpRayCheck = false;

                }
                else
                {

                   // Debug.Log("안부딪힘");

                }


            }


            PretmpY = transform.position.y;

        }




    }



    protected abstract void LandingEvent();
    protected bool Is_Skill_1_Attack = false;
    public virtual void SkillAttack_Anim_1_Enter() { }
    public virtual void SkillAttack_Anim_1_Exit() { }

  
    public virtual void SkillAttack_Anim_2_Enter() { }
    public virtual void SkillAttack_Anim_2_Exit() { }


    public virtual void SkillAttack_Anim_3_Enter() { }
    public virtual void SkillAttack_Anim_3_Exit() { }

    public virtual void Anim_Die_Enter() { }

    public abstract void DefaulAttack_Collider(GameObject obj);
    public abstract void Skill_1Attack_Collider(GameObject obj);
    public abstract void Skill_2Attack_Collider(GameObject obj);
    public abstract void Skill_3Attack_Collider(GameObject obj);
    public abstract void Skill_4Attack_Collider(GameObject obj);





}
