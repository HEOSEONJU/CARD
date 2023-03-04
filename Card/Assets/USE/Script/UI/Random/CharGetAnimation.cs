using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharGetAnimation : MonoBehaviour
{
    [Header("Information")]

    public PlayerInfos Data;
    public CharCardScriptTable CharDataBase;
    
    [Header("Text")]
    public CardGetUI PackText;

    [Header("Position")]
    public GameObject CardMovePosiObject;
    public List<Vector3> CardMovePosi;
    public GameObject CardSetObject;
    public List<SpellCard> CardObject;
    public Transform CenterPosi;
    public Transform CardOutPosi;
    public GameObject CardPosi;
    public List<Vector3> Card_Posi;
    public GameObject ParticleObject;
    public FailWinodw POPUP_1;

    
    public List<int> CharGet1;
    public List<int> CharGet2;
    public List<int> CharGet3;
    int Count;
    int CurrentCount;
    bool Action;
    bool CheckButton;



    [Header("����")]
    public GameObject OK_Button;
    
    public GameObject END_Button;

    

    public float CardDelay = 0.3f;
    private void Awake()
    {
        CardMovePosi = new List<Vector3>();//ī�尡���ʿ� ��ġ����ġ
        for (int i = 0; i < CardMovePosiObject.transform.childCount; i++)
        {
            CardMovePosi.Add(CardMovePosiObject.transform.GetChild(i).position);
        }
        CardObject = new List<SpellCard>();
        for (int i = 0; i < CardSetObject.transform.childCount; i++)//����ī��� ���μ��Ϳ���ġ
        {
            CardObject.Add(CardSetObject.transform.GetChild(i).GetComponent<SpellCard>());//����ī��κ���
            //CardObject[i].CardNumber = 0;
            CardObject[i].transform.position = CenterPosi.position;
            CardObject[i].transform.localScale = Vector3.zero;
            CardObject[i].Try_Find_Effect();

        }
        for (int i = 0; i < 10; i++)//ī����ü��ġ����ҹ޾ƿ���
        {
            Card_Posi.Add(CardPosi.transform.GetChild(i).transform.position);
        }
        CharGet1 = new List<int>();
        CharGet2 = new List<int>();
        CharGet3 = new List<int>();
        for (int i = 1; i < CharDataBase.Monster.Count; i++)
        {


            switch (CharDataBase.Monster[i].Rank)
            {
                case 1:
                    CharGet1.Add(CharDataBase.Monster[i].id);
                    break;
                case 2:
                    CharGet2.Add(CharDataBase.Monster[i].id);
                    break;
                case 3:
                    CharGet3.Add(CharDataBase.Monster[i].id);
                    break;
                default:
                    break;
            }

        }
    }

    private void OnEnable()
    {

        CheckButton = true;
        Action = false;
        Count = Data.UsePack;
        CurrentCount = Count;
        
        for (int i = 0; i < CardObject.Count; i++)//ī�尢���ʱ�ȭ
        {
            CardObject[i].transform.localPosition = Vector3.zero;
            CardObject[i].transform.rotation = Quaternion.Euler(0, 180, 0);
            CardObject[i].gameObject.SetActive(false);
            CardObject[i].transform.localScale = Vector3.zero;
            CardObject[i].BackCardPosi();

        }
        for (int i = 0; i < Data.UsePack; i++)//����ī�尹����ŭ ����ī��Ȱ��ȭ
        {
            CardObject[i].gameObject.SetActive(true);
        }
        END_Button.SetActive(false);
        if(Data.UsePack==1)
        {
            OK_Button.SetActive(false);
        }
        else
        {
            OK_Button.SetActive(true);
        }
        StartCoroutine(CardDrew());//�ٳѿ����� ī�����
    }


    public void CardDrewFunction()
    {

        if (Action == false & CheckButton)
        {
            StartCoroutine(CardNextToMove());
        }
        
    }
    public void EndGet()
    {
        transform.parent.transform.parent.transform.GetComponent<RandomScene>().MoveScene();//���̵�
        StartCoroutine(CardResetPositionCorountine());
    }
    IEnumerator CardResetPositionCorountine()
    {
        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < Data.UsePack; i++)//���Ѿ������ ī�� ����ġ�ΰ��� ���������
        {
            CardObject[i].transform.position = CenterPosi.position;
            CardObject[i].StartFlip();
        }

    }




    IEnumerator CardNextToMove()//ī�带������ �������ִ�ī��� ������ȭ��������̵�
    {

        Action = true;
        if (CurrentCount < 10 & (Data.UsePack != 1))
        {
            var target = CardObject[CurrentCount];
            PRS Posi = new PRS(CardOutPosi.position, Quaternion.Euler(0, -180, 0), Vector3.one * 45f);
            target.originPosi = Posi;
            
            target.MoveTransForm(target.originPosi, true, 0.7f);
            yield return new WaitForSeconds(0.7f);

            target.originPosi = new PRS(CardOutPosi.position, Quaternion.Euler(0, -180, 0), Vector3.one * 15f);
            target.MoveTransForm(target.originPosi, false);
        }
        StartCoroutine(CardOpenCoroutine());//ȭ����̵��� ���ʿ��ִ� ī�� �߾ӿ� ��ġ

    }
    IEnumerator CardOpenCoroutine()//ī��1�徿���� 9������0������ ���ΰ����ߴٸ� ���κ����ִ°����ι�ġ 
    {
        CurrentCount -= 1;
        if (CurrentCount >= 0)
        {
            PRS Posi = new PRS(CenterPosi.position, Quaternion.Euler(0, -180, 0), Vector3.one * 45f);

            var target = CardObject[CurrentCount];
            target.originPosi = Posi;
            target.Change_Effect_Size(1);
            target.MoveTransForm(target.originPosi, true, 0.7f);
            yield return new WaitForSeconds(0.7f);
            CardObject[CurrentCount].StartFlip();
            Action = false;
            if (Data.UsePack == 1)//1�ѱ�°Ŷ�� ��ü��������������
            {
                CheckButton = false;
                
                END_Button.SetActive(true);
            }
        }

        else
        {
            PRS Posi;

            for (int i = 0; i < Data.UsePack; i++)
            {

                Posi = new PRS(Card_Posi[i], Quaternion.Euler(0, -180, 0), Vector3.one * 15f);

                var target = CardObject[i];
                target.Change_Effect_Size(0.5f);
                target.originPosi = Posi;
                
                target.MoveTransForm(target.originPosi, true, 0.7f);


            }
            StartCoroutine(Button_Delay(1.0f));

            CheckButton = false;
            Action = false;
        }





    }
    public void SkipButton()//1��Ŀ��� ��ü������
    {


        if (CheckButton & (!Action))
        {
            PRS Posi;
            for (int i = 0; i < Data.UsePack; i++)
            {
                if (CurrentCount <= i)
                {
                    Posi = new PRS(Card_Posi[i], Quaternion.Euler(0, -180, 0), Vector3.one * 15f);

                }
                else
                {
                    Posi = new PRS(Card_Posi[i], Quaternion.Euler(0, -180, 0), Vector3.one * 15f);
                    CardObject[i].StartFlip();
                }


                var target = CardObject[i];

                target.originPosi = Posi;
                target.Change_Effect_Size(0.5f);
                target.MoveTransForm(target.originPosi, true, 0.7f);


            }
            
            CheckButton = false;
            StartCoroutine(Button_Delay(1.0f));
        }
    }
    IEnumerator Button_Delay(float time)
    {
        yield return new WaitForSeconds(time);
        OK_Button.SetActive(false);
        END_Button.SetActive(true);


    }
    

    public void CardOpen()
    {
        CurrentCount -= 1;

        PRS Posi = new PRS(CenterPosi.position, Quaternion.Euler(0, -180, 0), Vector3.one * 45f);

        var target = CardObject[CurrentCount];
        target.originPosi = Posi;
        target.MoveTransForm(target.originPosi, true, 0.7f);

    }



    IEnumerator CardDrew()//��í���� �������� ī�������Է�
    {
        Action = true;




        for (int i = 0; i < Count; i++)//ī���̹����Է�
        {

            

            
            CardObject[i].transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Image>().sprite = CharDataBase.Monster[Data.GetPack[i]].BGImage;//�����̹���
            Image Temp_Image = CardObject[i].transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Image>();//�����̹���
            Temp_Image.sprite = CharDataBase.Monster[Data.GetPack[i]].Image;
            CardObject[i].transform.GetChild(1).GetChild(0).GetChild(4).GetChild(1).GetComponent<Image>().sprite = CharDataBase.Monster[Data.GetPack[i]].RankImage;
            CardObject[i].transform.GetChild(1).GetChild(0).GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = CharDataBase.Monster[Data.GetPack[i]].CardName;
            Image Temp_Tilte_Image = CardObject[i].transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>();
            Temp_Image.material=CharDataBase.Monster[Data.GetPack[i]].material;//3�� �����̹����׵θ��߰�


            if (CharDataBase.Monster[Data.GetPack[i]].Rank == 3)
            {
                CardObject[i].Reset_Particle();
                Temp_Tilte_Image.enabled = false;
                Temp_Tilte_Image.material =CharDataBase.Monster[Data.GetPack[i]].Title_material; //3���׵θ����׸����߰��ϴ¹���
                Debug.Log("3��ȹ�� ����ƮȰ��ȭ");
                
            }
            else if (CharDataBase.Monster[Data.GetPack[i]].Rank == 2)
            {
                Temp_Tilte_Image.enabled = false;
                Temp_Tilte_Image.material =CharDataBase.Monster[Data.GetPack[i]].Title_material; //2���׵θ����׸����߰��ϴ¹���
                CardObject[i].Stop_Particle();

                
            }
            else
            {
                CardObject[i].Stop_Particle();
                Temp_Tilte_Image.enabled = false;

            }


        }


        if (Data.UsePack == 1)
        {
            StartCoroutine(CardNextToMove());
            yield return new WaitForSeconds(CardDelay);
        }
        else
        {
            for (int i = 0; i < Count; i++)//ī����������������� ��ġ
            {
                CardAlignment(i);
                yield return new WaitForSeconds(CardDelay);
            }
        }
        
        //ParticleON_OFF(false);
        Action = false;


    }

    void CardAlignment(int num)//ī��������ġ��
    {
        PRS Posi = new PRS(CardMovePosi[num], Quaternion.Euler(0, -180, 0), Vector3.one * 15f);

        var target = CardObject[num];
        target.originPosi = Posi;
        target.MoveTransForm(target.originPosi, true, 0.7f);



    }



    public void Reset()//�ٽ� �̱�
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
                

                for (int i = 0; i < Data.UsePack; i++)
                {
                    CardObject[i].transform.position = CenterPosi.position;
                    CardObject[i].transform.localScale = Vector3.zero;
                    CardObject[i].BackCardPosi();

                }


                Data.GetPack.Clear();
                for (int i = 0; i < Data.UsePack; i++)
                {
                    int Gold = 0;

                    int num = Random.Range(1, 100);
                    if (num >= 90)
                    {
                        Gold = 10000;
                        num = CharGet3[Random.Range(0, CharGet3.Count)];
                    }
                    else if (num >= 50)
                    {
                        
                            Gold = 200;
                            num = CharGet2[Random.Range(0, CharGet2.Count)];
                        }
                    else
                    {
                            Gold = 100;
                            num = CharGet1[Random.Range(0, CharGet1.Count)];
                        }

                    bool ck = true;
                    
                    for (int j = 0; j < Data.MonsterCards.Count; j++)
                    {
                        if (num == Data.MonsterCards[j].ID)
                        {
                            //�ߺ�Ž������
                            if (Data.MonsterCards[j].BreaK_Lim <= 4)
                            {
                                Data.MonsterCards[j].BreaK_Lim++;

                            }
                            else//�Ѱ赹�İ����������
                            {
                                Data.Gold += Gold;
                            }
                            ck = false;
                            break;
                        }


                    }
                    if (ck == true)//�ߺ��̾ƴ�
                    {
                        MonsterInfo temp = new MonsterInfo();
                        temp.Init_Monset(num);
                        
                        Data.MonsterCards.Add(temp);
                    }
                    
                    Data.GetPack.Add(num);
                    
                    Data.SpellCardPack -= 1;

                }

                //PackCount -= cost;
                //PackText.text = "" + PackCount;
                //Data.SpellCardPack = PackCount;


                Count = Data.UsePack;
                CurrentCount = Count;
                PackText.ResetPackText();
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
