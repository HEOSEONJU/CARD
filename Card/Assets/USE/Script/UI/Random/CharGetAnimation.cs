using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharGetAnimation : GetAnimation
{

    [SerializeField] 
    List<Image> Bg;//바탕이미지
    [SerializeField]
    List<Image> Main;//캐릭터 일러스트
    [SerializeField]
    List<Image> OutLine;//외곽 마테리얼


    protected override IEnumerator CardDrew()//가챠겟팩 값에의한 카드정보입력
    {
        Action = true;
        for (int i = 0; i < Data.GetPack.Count; i++)//카드이미지입력
        {
            Bg[i].sprite = CardData.instance.CharDataFile.Monster[Data.GetPack[i]].BGImage;
            Main[i].sprite = CardData.instance.CharDataFile.Monster[Data.GetPack[i]].Image;
            OutLine[i].material = CardData.instance.CharDataFile.Monster[Data.GetPack[i]].Title_material;
            #region 카드랭크별 이펙트
            switch (CardData.instance.CharDataFile.Monster[Data.GetPack[i]].Rank)
            {
                case 3:
                    CardObject[i].Reset_Particle();
                    OutLine[i].enabled = true;
                    break;
                case 2:
                    CardObject[i].Stop_Particle();
                    OutLine[i].enabled = true;
                    break;
                default:
                    CardObject[i].Stop_Particle();
                    OutLine[i].enabled = false;
                    break;
            }
            #endregion
        }
        #region 카드 1뽑 10뽑인지 구분후 연출
        switch (Data.UsePack) //카드 1뽑 10뽑 구분해서 연출
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

                    int num = cardGetUI.Random_Result(Random.Range(1, 100), true, out Gold);
                    
                    MonsterInfo T = Data.MonsterCards.Find(x => x.ID == num);
                    if (T != null)
                    {
                        if (T != null)
                        {
                            cardGetUI.Check_Char_Limit(T, Gold);
                        }
                    }
                    else
                    {
                        MonsterInfo temp = new MonsterInfo();
                        temp.Init_Monset(num);
                        Data.MonsterCards.Add(temp);
                    }
                    Data.GetPack.Add(num);
                    Data.SpellCardPack -= 1;
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
