using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class Spell_GetAnimation : GetAnimation
{
    [SerializeField]
    List<Image> Bg;
    [SerializeField]
    List<Image> Main;
    [SerializeField]
    List<TextMeshProUGUI> Cost_Text;
    [SerializeField]
    List<TextMeshProUGUI> Card_Name;
    [SerializeField]
    List<TextMeshProUGUI> Exp;
    

    

    protected override IEnumerator CardDrew()//가챠겟팩 값에의한 카드정보입력
    {
        Action = true;
        for (int i = 0; i < Data.UsePack; i++)//뽑은 갯수만큼 카드이미지입력
        {
            Bg[i].sprite = CardData.instance.CardDataFile.cards[Data.GetPack[i]].BG_Image;//바탕이미지
            Main[i].sprite= CardData.instance.CardDataFile.cards[Data.GetPack[i]].Image;//바탕이미지
            Cost_Text[i].text = CardData.instance.CardDataFile.cards[Data.GetPack[i]].cost.ToString();//코스트갱신
            Card_Name[i].text = CardData.instance.CardDataFile.cards[Data.GetPack[i]].CardName;//이름갱신
            Exp[i].text = CardData.instance.CardDataFile.cards[Data.GetPack[i]].exp;//설명갱신
            #region 카드 3랭크면 파티클이펙트적용
            switch (CardData.instance.CardDataFile.cards[Data.GetPack[i]].Rank)//랭크가3이면 파티클활성화
            {
                case 3:
                    CardObject[i].Reset_Particle();
                    break;
                default:
                    CardObject[i].Stop_Particle();
                    break;
            }
            #endregion
        }
        #region 카드 뽑기 1뽑 10뽑 구분하고 연출

        switch (Data.UsePack)//1뽑 10뽑 연출구분해서 보여주기
        {
            case 1:
                StartCoroutine(CardNextToMove());
                yield return new WaitForSeconds(CardDelay);
                break;
            default:
                for (int i = 0; i < Data.UsePack; i++)//카드차곡차곡왼쪽으로 배치
                {
                    CardAlignment(i);
                    yield return new WaitForSeconds(CardDelay);
                }
                break;
        }
        #endregion
        Action = false;
        
    }

    public override void Reset()//다시 뽑기
    {
        if (CheckButton == false)
        {
            if (Data.SpellCardPack >= Data.UsePack)
            {
                
                if (Data.UsePack != 1)
                {
                    OK_Button.SetActive(true);
                }
                END_Button.SetActive(false);
                CheckButton = true;

                Card_Positin_Reset();


                
                Data.GetPack.Clear();



                for (int i = 0; i < Data.UsePack; i++)
                {
                    int Gold = 0;
                    

                    int num= cardGetUI.Random_Result(Random.Range(1, 100), false, out Gold);

                    if (cardGetUI.Check_Spell_Count(num)) //3장미만일경우
                    {
                        Data.HaveCard.Add(num);
                    }
                    else//3장이상인경우
                    {
                        Data.Gold += Gold;
                    }
                    Data.GetPack.Add(num);
                }
                
                CurrentCount = Data.UsePack;
                cardGetUI.ResetPackText();
                StartCoroutine(CardDrew());//다시뽑기
                CheckButton = true;
                Data.Saved_Data();
            }
            else
            {
                POPUP_1.View_Text("보유 티켓이 모자랍니다.");
            }
        }
    }
}

