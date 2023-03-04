using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class CharManager : MonoBehaviour
{
    Manager DataManager;
    public GameObject MonsterCard;
    public GameCard SelectedCard;
    public GameObject Infomation;
    public List<GameCard> CombatChar;
    public List<TextMeshProUGUI> TextList;
    [SerializeField]
    Transform Contents_Parent;


    //bool CanAttack;

    private void Awake()
    {
        DataManager = GetComponent<Manager>();

        //CanAttack = true;

        CombatChar = new List<GameCard>();
        for (int i = 0; i < DataManager.MYDeck.Data.UseMonsterCards.Count; i++)
        {
            int temp = DataManager.MYDeck.Data.UseMonsterCards[i];
            CombatChar.Add(MonsterCard.transform.GetChild(i).GetComponent<GameCard>());
        }
    }
    private void OnEnable()
    {
        Infomation.SetActive(false);
        //MonsterCardReset();
    }


    public void CharInit()
    {
        for (int i = 0; i < DataManager.MYDeck.Data.UseMonsterCards.Count; i++)
        {
            
            MonsterCard.transform.GetChild(i).GetComponent<GameCard>().InputID(DataManager.MYDeck.Data.UseMonsterCards[i], DataManager.MYDeck.Data);
            
        }
    }
    
    public IEnumerator MonsterCardReset(float DelayTime=3.0f)
    {
        yield return new WaitForSeconds(DelayTime);
        for (int i = 0; i < DataManager.MYDeck.Data.UseMonsterCards.Count; i++)
        {

            int temp = DataManager.MYDeck.Data.UseMonsterCards[i];
            GameCard MC = MonsterCard.transform.GetChild(i).GetComponent<GameCard>();
            for(int j=0;j<MC.Skill_ID.Count;j++)
            {
                
                if (MC.SkillDataBase.Skill[MC.Skill_ID[j]].id==0)//0번은 없는스킬
                {
                    continue;
                }
                
                //Debug.Log((i+1) + "번쨰캐릭터 " + (j+1) + "번째스킬" +MC.Skill_ID[j]+"<아이디 /"+ MC.SkillDataBase.Skill[MC.Skill_ID[j]].CardType+"대상번호");
                if (MC.SkillDataBase.Skill[MC.Skill_ID[j]].CardType == 0)
                {

                    
                    bool c = false;//이미적용되있는지 판단하는변수

                    foreach(SpellEffect SE in DataManager.Enemy_Manager.Skill_Effects_List)
                    {
                        if (SE.ID == MC.SkillDataBase.Skill[MC.Skill_ID[j]].id)
                        {
                            c = true;
                            break;
                        }
                    }


                    if (c == false)
                    {
                        DataManager.Enemy_Manager.UsingSkill(MC.SkillDataBase.Skill[MC.Skill_ID[j]]);//적에게
                    }
                }
                else if (MC.SkillDataBase.Skill[MC.Skill_ID[j]].CardType == 1)
                {
                    GameCard temp_multi = MonsterCard.transform.GetChild(i).GetComponent<GameCard>();
                    
                    bool c = false;
                    foreach (SpellEffect SE in temp_multi.Skill_Effects)
                    {
                        if(SE.ID== MC.SkillDataBase.Skill[MC.Skill_ID[j]].id)
                        {
                            c = true;
                            //Debug.Log("이미적용된효과");
                            break;
                        }
                    }
                    
                    if (c == false)
                    {

                        temp_multi.SkillApply(MC.SkillDataBase.Skill[MC.Skill_ID[j]], MC.Skill_ID[j]);
                    }
                }
                else if (MC.SkillDataBase.Skill[MC.Skill_ID[j]].CardType == 2)
                {
                    for(int k=0;k<DataManager.MYDeck.Data.UseMonsterCards.Count;k++)
                    {
                        GameCard temp_multi = MonsterCard.transform.GetChild(k).GetComponent<GameCard>();
                        
                        bool c = false;


                        foreach (SpellEffect SE in temp_multi.Skill_Effects)
                        {
                            if (SE.ID == MC.SkillDataBase.Skill[MC.Skill_ID[j]].id)
                            {
                                c = true;
                                //Debug.Log("이미적용된효과");
                                break;
                            }
                        }


                        if (c == false)
                        {
                            
                            temp_multi.SkillApply(MC.SkillDataBase.Skill[MC.Skill_ID[j]], MC.Skill_ID[j]);
                        }
                    }
                }
                else if (MC.SkillDataBase.Skill[MC.Skill_ID[j]].CardType == 3)
                {
                    
                    SpellCard e = new SpellCard();
                    e.Skill_Apply(MC.SkillDataBase.Skill[MC.Skill_ID[j]]);
                    DataManager.Use_MY_Effect(e,true);
                    if(e.Value_Create_Deck>0)
                    {
                        DataManager.LoadingTimer += 0.2f * e.Value_Create_Deck;
                        yield return new WaitForSeconds(0.2f*e.Value_Create_Deck);
                    }
                }

            }
            DataManager.LoadingTimer += 0.5f ;
            yield return new WaitForSeconds(0.2f);
        }

        DataManager.LoadingTimer = 0;
        DataManager.Enemy_Manager.Image_buff();
    }

    public void TurnEnd()
    {
        for (int i = 0; i < DataManager.MYDeck.Data.UseMonsterCards.Count; i++)
        {


            MonsterCard.transform.GetChild(i).GetComponent<GameCard>().TURNUSE();
        }


    }

    public int MAX_Drew_Count()
    {
        int Count = 1;
        for (int i = 0; i < DataManager.MYDeck.Data.UseMonsterCards.Count; i++)
        {


            if (Count < MonsterCard.transform.GetChild(i).GetComponent<GameCard>().MAX_DREW_Count())
                Count = MonsterCard.transform.GetChild(i).GetComponent<GameCard>().MAX_DREW_Count();
        }


        return Count;
    }

    public void Renge_HP_Effect()
    {
        for (int i = 0; i < DataManager.MYDeck.Data.UseMonsterCards.Count; i++)
        {


            MonsterCard.transform.GetChild(i).GetComponent<GameCard>().REGEN_Active();
        }

    }


    void InformationCard(bool INFO, GameCard Card)
    {

        if (INFO & Card != null)
        {
            //Debug.Log("선택된아이디:" + Card.ID);
            Infomation.SetActive(true);
            Card.CalculatorStatus();

            TextList[0].text = "ATK:" + Card.Current_ATK;
            TextList[1].text = "DEF:" + Card.Current_DEF;
            TextList[2].text = "HP:" + Card.Current_HP;
            TextList[3].text = "MR:" + Card.Effect_Magic_Regi + "%";
            TextList[4].text = "CP:" + Card.Current_CriticalPercent + "%";
            TextList[5].text = "CD:" + (100 + Card.Current_CriticalDamage) + "%";

            Color color_0 = Color.white;
            color_0.a = 0;
            Color color_1 = Color.white;
            color_1.a = 1;
            int C_Index=0;
            foreach (SpellEffect SE in Card.Skill_Effects)
            {
                Image temp = Contents_Parent.GetChild(C_Index++).GetComponent<Image>();
                temp.color = color_1;

                        temp.sprite = CardData.instance.CharSkillFile.Skill[SE.ID].Image;
                     
                    
                


            }
            
            foreach (SpellEffect SE in Card.Spell_Effects)
            {
                Image temp = Contents_Parent.GetChild(C_Index++).GetComponent<Image>();
                temp.color = color_1;

                temp.sprite = CardData.instance.CardDataFile.Find_Spell_Card(SE.ID).Image;
                        
                    
                


            }

            for (int i = C_Index; i < Contents_Parent.childCount; i++)
            {

                Contents_Parent.GetChild(i).GetComponent<Image>().color = color_0;




            }

        }
        else
        {
            Infomation.SetActive(false);
        }
    }











    public void CardMouseOver(GameCard Card)//카드확대
    {
        if (SelectedCard == null & !DataManager.STOP)
        {
            SelectedCard = Card;
            InformationCard(true, Card);
        }
    }
    public void CardMouseExit(GameCard Card)
    {
        InformationCard(false, Card);
        SelectedCard = null;
    }

    public void CardMouseDown()
    {

    }

    public void CardMouseUp()
    {


    }


}

