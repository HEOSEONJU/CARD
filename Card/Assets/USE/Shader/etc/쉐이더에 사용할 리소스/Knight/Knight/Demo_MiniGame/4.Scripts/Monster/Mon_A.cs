using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lofle;


public class Mon_A :Mon_Bass
{

    protected StateMachine<Mon_A> _stateMachine = null;

    



    //public PhotonView m_Photonview;

    public override void Init()
    {
       
             _stateMachine = new StateMachine<Mon_A>(this);
            StateCo = StartCoroutine(_stateMachine.Coroutine<RunState>());

     
    }

    public override void DefaulAttack_Collider(GameObject obj)
    {
    

        if (obj.CompareTag("Player"))
        {
            if (obj.CompareTag("Player")) // 맞는 처리는 서버에서만 보내준다. 
            {
    

            }
          
        }


    }


    public override void Skill_1Attack_Collider(GameObject obj)
    {

      



    }
    public override void Skill_2Attack_Collider(GameObject obj)
    {

      



    }
    public override void Skill_3Attack_Collider(GameObject obj)
    {

   



    }
    public override void Skill_4Attack_Collider(GameObject obj)
    {

    }


    public bool b_DefaultAttack_Anim = false;

    public override void DefaultAttack_Anim_1_Enter()
    {
 
        b_DefaultAttack_Anim = true;

    }

    public override void DefaultAttack_Anim_1_Exit()
    {
        b_DefaultAttack_Anim = false;
    }



    void Update()
    {
      
       
    }

 
    public override void Damaged(float DamageValue, Vector2 dir, float stunTime)
    {

        if (Isdie)
            return;

        if (stunTime > 0)
            HittedFuc(stunTime);

        m_rigidbody2D.velocity = new Vector2(0, 0);
        m_rigidbody2D.AddForce(dir, ForceMode2D.Impulse);

 

        float PreHP = m_HP;
        m_HP -= DamageValue;


       // if (DamageValue > 0)
            SetCreateBloodEffect(DamageValue);


        SyncHp(m_HP);
  

        if (m_HP <= 0)
        {
         
            StopCoroutine(StateCo);
            StateCo = StartCoroutine(_stateMachine.Coroutine<DieState>());



        }

    }



    public float StuneTime;
    public override void HittedFuc(float stunTime)
    {
        StuneTime = stunTime;
        StopCoroutine(StateCo);
        StateCo = StartCoroutine(_stateMachine.Coroutine<HitState>());
  
    }




    private class IdleState : State<Mon_A>
    {
        protected override void Begin()
        {
            Owner.SetAnim("Demo_Idle");
            TimeTic = 0;
            Owner.MoveDir = Vector2.zero;
        }

        private float RandomTime=0.2f;
        private float TimeTic = 0;
        protected override void Update()
        {

            TimeTic += Time.deltaTime;
            if (TimeTic > RandomTime)
            {
                TimeTic = 0;


                if (Owner.Current_Tartget != null)
                {
         

                    float CurrentEnermyDis = Vector2.Distance(Owner.Current_Tartget.transform.position, Owner.transform.position);

                    if (CurrentEnermyDis <= Owner.AttackDis)
                    {
                     //   Invoke<AttackState>();
                    }
                    else
                    {
                        Invoke<RunState>();
                    }

                }
                else
                {
                    Invoke<IdleState>();

                }

            }



        }

        protected override void End()
        {

        }

    }


    private class RunState : State<Mon_A>
    {
        protected override void Begin()
        {
         
            Owner.SetAnim("Demo_Run");
           


        }

        private float updateTimeTic = 0;
        private float updateTime = 0.1f;

        protected override void Update()
        {


            if (Owner.Current_Tartget == null)
            {
                Invoke<IdleState>();
                return;
            }
              
            Owner.Move();
         


            updateTimeTic += Time.deltaTime;
            if (updateTimeTic > updateTime)
            {
                updateTimeTic = 0;

         
                float CurrentEnermyDis = Vector2.Distance(Owner.Current_Tartget.transform.position, Owner.transform.position);

                if (CurrentEnermyDis <= Owner.AttackDis)
                {
                    Invoke<IdleState>();
                }
        

                
            }

        }

        protected override void End()
        {

        }
    }

    
   
    private class HitState : State<Mon_A>
    {
        protected override void Begin()
        {
            Owner.SetAnim("Demo_Hit");
            TimeTic = 0;
            Owner.MoveDir = Vector2.zero;
        }

        private float TimeTic = 0;
        protected override void Update()
        {
         

            TimeTic += Time.deltaTime;
            if (TimeTic > Owner.StuneTime)
            {
                TimeTic = 0;

   
                Invoke<IdleState>();


            }
        }

        protected override void End()
        {

        }

    }






    private class DieState : State<Mon_A>
    {
        protected override void Begin()
        {
            Owner.SetAnim("Demo_Die");
            DieTime = 0.5f;
            Owner.MoveDir = Vector2.zero;
        }
        private float DieTime = 0.5f;
        private float TimeTic = 0;
        protected override void Update()
        {
          
            TimeTic += Time.deltaTime;
            if (TimeTic > DieTime)
            {
                TimeTic = 0;
                Demo_GM.Gm.MonsterList.Remove(Owner.gameObject);
                Destroy(Owner.gameObject);
            }

        }

        protected override void End()
        {

        }

    }


}
