using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharGetAnimation : GetAnimation
{

    [SerializeField] 
    List<Image> Bg;//�����̹���
    [SerializeField]
    List<Image> Main;//ĳ���� �Ϸ���Ʈ
    [SerializeField]
    List<Image> OutLine;//�ܰ� ���׸���


    protected override IEnumerator CardDrew()//��í���� �������� ī�������Է�
    {
        Action = true;
        for (int i = 0; i < Data.GetPack.Count; i++)//ī���̹����Է�
        {
            Bg[i].sprite = CardData.instance.CharDataFile.Monster[Data.GetPack[i]].BGImage;
            Main[i].sprite = CardData.instance.CharDataFile.Monster[Data.GetPack[i]].Image;
            OutLine[i].material = CardData.instance.CharDataFile.Monster[Data.GetPack[i]].Title_material;
            #region ī�巩ũ�� ����Ʈ
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
        #region ī�� 1�� 10������ ������ ����
        switch (Data.UsePack) //ī�� 1�� 10�� �����ؼ� ����
        {
            case 1:
                StartCoroutine(CardNextToMove());
                yield return new WaitForSeconds(CardDelay);
                break;
            default:
                for (int i = 0; i < Data.UsePack; i++)//ī����������������� ��ġ
                {
                    CardAlignment(i);
                    yield return new WaitForSeconds(CardDelay);
                }
                break;

        }
        #endregion


        Action = false;
    }

    public override void Reset()//�ٽ� �̱�
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
                StartCoroutine(CardDrew());//�ٽû̱�
                CheckButton = true;
                Data.Saved_Data();
            }
            else
            {
                POPUP_1.View_Text("���� Ƽ���� ���ڶ��ϴ�.");
            }
        }
    }
}
