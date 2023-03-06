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
    

    

    protected override IEnumerator CardDrew()//��í���� �������� ī�������Է�
    {
        Action = true;
        for (int i = 0; i < Data.UsePack; i++)//���� ������ŭ ī���̹����Է�
        {
            Bg[i].sprite = CardData.instance.CardDataFile.cards[Data.GetPack[i]].BG_Image;//�����̹���
            Main[i].sprite= CardData.instance.CardDataFile.cards[Data.GetPack[i]].Image;//�����̹���
            Cost_Text[i].text = CardData.instance.CardDataFile.cards[Data.GetPack[i]].cost.ToString();//�ڽ�Ʈ����
            Card_Name[i].text = CardData.instance.CardDataFile.cards[Data.GetPack[i]].CardName;//�̸�����
            Exp[i].text = CardData.instance.CardDataFile.cards[Data.GetPack[i]].exp;//������
            #region ī�� 3��ũ�� ��ƼŬ����Ʈ����
            switch (CardData.instance.CardDataFile.cards[Data.GetPack[i]].Rank)//��ũ��3�̸� ��ƼŬȰ��ȭ
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
        #region ī�� �̱� 1�� 10�� �����ϰ� ����

        switch (Data.UsePack)//1�� 10�� ���ⱸ���ؼ� �����ֱ�
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
                    

                    int num= cardGetUI.Random_Result(Random.Range(1, 100), false, out Gold);

                    if (cardGetUI.Check_Spell_Count(num)) //3��̸��ϰ��
                    {
                        Data.HaveCard.Add(num);
                    }
                    else//3���̻��ΰ��
                    {
                        Data.Gold += Gold;
                    }
                    Data.GetPack.Add(num);
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

