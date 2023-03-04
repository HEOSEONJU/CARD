using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimDemo_GM : MonoBehaviour {

	
    public int CurrentIndex = 1;
    public int MaxIndex = 5;

    //public Text CurrentStateText;
    public Sprite[] NumGroup;
    public Sprite[] StateTextImg;

    public Image StateImg;
    public Image NumImg;
    public void PreBtn()
    {
        CurrentIndex--;
        if (CurrentIndex <= 1)
        {
            CurrentIndex = 1;
        }
        ChangeAnim(CurrentIndex);
    }

    public void NextBtn()
    {
        CurrentIndex++;
        if (CurrentIndex >= MaxIndex)
        {
            CurrentIndex = MaxIndex;
        }
        ChangeAnim(CurrentIndex);
    }

   
    public Animator[] AnimArray;

    void ChangeAnim(int m_idx)
    {

        switch (m_idx)
        {
            case 1: //Idle
                for (int i = 0; i < AnimArray.Length; i++)
                {
                    AnimArray[i].Play("Idle");
                }
             

                StateImg.sprite = StateTextImg[0];
             
                NumImg.sprite = NumGroup[1];      
                break;
            case 2: //Sit
                for (int i = 0; i < AnimArray.Length; i++)
                {
                    AnimArray[i].Play("Sit");
                }

          
                StateImg.sprite = StateTextImg[1];
            
                NumImg.sprite = NumGroup[2];
                break;
            case 3: //Jump
                for (int i = 0; i < AnimArray.Length; i++)
                {
                    AnimArray[i].Play("Jump");
                }


                StateImg.sprite = StateTextImg[2];
         
                NumImg.sprite = NumGroup[3];
                break;
            case 4: //Run

                for (int i = 0; i < AnimArray.Length; i++)
                {
                    AnimArray[i].Play("Run");
                }

             

                StateImg.sprite = StateTextImg[3];
            
                NumImg.sprite = NumGroup[4];
                break;
            case 5: //Attack

                for (int i = 0; i < AnimArray.Length; i++)
                {
                    AnimArray[i].Play("Attack");
                }

             
                
                StateImg.sprite = StateTextImg[4];
               
                NumImg.sprite = NumGroup[5];
                break;
            case 6: //Guard

                for (int i = 0; i < AnimArray.Length; i++)
                {
                    AnimArray[i].Play("Guard");
                }

              
                StateImg.sprite = StateTextImg[5];
               
                NumImg.sprite = NumGroup[6];
                break;
            case 7: //Hit

                for (int i = 0; i < AnimArray.Length; i++)
                {
                    AnimArray[i].Play("Hit");
                }


                StateImg.sprite = StateTextImg[6];
           
                NumImg.sprite = NumGroup[7];
                break;
            case 8: //Die


                for (int i = 0; i < AnimArray.Length; i++)
                {
                    AnimArray[i].Play("Type"+(i+1)+"_Die");
                }
             

                StateImg.sprite = StateTextImg[7];
        
                NumImg.sprite = NumGroup[8];
                break;
            case 9: //Skill_1
                for (int i = 0; i < AnimArray.Length; i++)
                {
                    AnimArray[i].Play("Skill_1");
                }
              
                
                StateImg.sprite = StateTextImg[8];
   
                NumImg.sprite = NumGroup[9];
                break;
            case 10: //Skill_2
                for (int i = 0; i < AnimArray.Length; i++)
                {
                    AnimArray[i].Play("Skill_2");
                }
               

                StateImg.sprite = StateTextImg[9];

                NumImg.sprite = NumGroup[10];
                break;
            case 11: //Skill_3
                for (int i = 0; i < AnimArray.Length; i++)
                {
                    AnimArray[i].Play("Skill_3");
                }
            

                StateImg.sprite = StateTextImg[10];

                NumImg.sprite = NumGroup[11];
                break;

        }



    }
}
