using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardGetAnimation : MonoBehaviour
{
    [Header("Information")]

    public PlayerInfos Data;
    public CardScriptable CardDataBase;
    [SerializeField]
    List<Material> materials;

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

    public List<int> CardGet1;
    public List<int> CardGet2;
    public List<int> CardGet3;
    int Count;
    int CurrentCount;
    bool Action;
    bool CheckButton;
    public float CardDelay = 0.3f;



    [Header("버툰")]
    public GameObject OK_Button;
    public GameObject End_Button;


    private void Awake()
    {
        CardMovePosi = new List<Vector3>();//카드가왼쪽에 배치될위치
        for (int i = 0; i < CardMovePosiObject.transform.childCount; i++)
        {
            CardMovePosi.Add(CardMovePosiObject.transform.GetChild(i).position);
        }
        CardObject = new List<SpellCard>();
        for (int i = 0; i < CardSetObject.transform.childCount; i++)//리얼카드들 전부센터에배치
        {
            CardObject.Add(CardSetObject.transform.GetChild(i).GetComponent<SpellCard>());
            //CardObject[i].CardNumber = UseData.GetPack[i];
            CardObject[i].transform.position = CenterPosi.position;
            CardObject[i].Try_Find_Effect();

        }
        for (int i = 0; i < 10; i++)//카드전체배치될장소받아오기
        {
            Card_Posi.Add(CardPosi.transform.GetChild(i).transform.position);
        }
        CardGet1 = new List<int>();
        CardGet2 = new List<int>();
        CardGet3 = new List<int>();
        for (int i = 1; i < CardDataBase.cards.Count; i++)
        {
            switch (CardDataBase.cards[i].Rank)
            {
                case 1:
                    CardGet1.Add(CardDataBase.cards[i].id);
                    break;
                case 2:
                    CardGet2.Add(CardDataBase.cards[i].id);
                    break;
                case 3:
                    CardGet3.Add(CardDataBase.cards[i].id);
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

        CardPosiReset();
        for (int i = 0; i < Data.UsePack; i++)//뽑은카드갯수만큼 리얼카드활성화
        {
            CardObject[i].gameObject.SetActive(true);
            CardObject[i].Reset_Particle();
        }
        End_Button.SetActive(false);
        if (Data.UsePack == 1)
        {
            OK_Button.SetActive(false);
        }
        else
        {
            OK_Button.SetActive(true);
        }
        StartCoroutine(CardDrew());//겟넘에의한 카드생성
    }

    void CardPosiReset()
    {
        for (int i = 0; i < CardObject.Count; i++)//카드각도초기화
        {
            CardObject[i].transform.localPosition = Vector3.zero;
            CardObject[i].transform.rotation = Quaternion.Euler(0, 180, 0);
            CardObject[i].gameObject.SetActive(false);
            CardObject[i].BackCardPosi();
            
            PRS Posi_temp = new PRS(CardObject[i].transform.position, CardObject[i].transform.rotation, Vector3.zero);
            CardObject[i].originPosi = Posi_temp;



            CardObject[i].MoveTransForm(CardObject[i].originPosi, false);
        }
    }


    public void CardDrewFunction()
    {

        if (Action == false & CheckButton)
        {
            StartCoroutine(CardNextToMove());
        }
        else if (CheckButton == false)
        {

        }
    }

    public void CardGetEnd()
    {
        transform.parent.transform.parent.transform.GetComponent<RandomScene>().MoveScene();//씬이동
        StartCoroutine(CardResetPositionCorountine());
    }

    IEnumerator CardResetPositionCorountine()
    {
        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < Data.UsePack; i++)//씬넘어가기전에 카드 제위치로가고 뒤집어놓기
        {
            CardObject[i].transform.position = CenterPosi.position;
            CardObject[i].StartFlip();

        }

    }
    IEnumerator CardNextToMove()//카드를뽑을때 기존에있던카드는 오른쪽화면밖으로이동
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
        StartCoroutine(CardOpenCoroutine());//화면밖이동후 왼쪽에있던 카드 중앙에 배치

    }
    IEnumerator CardOpenCoroutine()//카드1장씩개방 9번부터0번까지 전부개방했다면 전부보여주는것으로배치 
    {
        CurrentCount -= 1;
        if (CurrentCount >= 0)
        {
            PRS Posi = new PRS(CenterPosi.position, Quaternion.Euler(0, -180, 0), Vector3.one * 45f);

            var target = CardObject[CurrentCount];
            target.originPosi = Posi;
            target.MoveTransForm(target.originPosi, true, 0.7f);
            target.Change_Effect_Size(1);
            yield return new WaitForSeconds(0.7f);
            CardObject[CurrentCount].StartFlip();
            Action = false;
            if (Data.UsePack == 1)//1팩까는거라면 전체펴지기하지않음
            {
                CheckButton = false;
                End_Button.SetActive(true);
            }
        }

        else//카드펼치기
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
    public void SkipButton()//1장식에서 전체공개로
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
            StartCoroutine(Button_Delay(1.0f));

            CheckButton = false;
        }
    }

    IEnumerator Button_Delay(float time)
    {
        yield return new WaitForSeconds(time);
        OK_Button.SetActive(false);
        End_Button.SetActive(true);


    }

    public void CardOpen()
    {
        CurrentCount -= 1;

        PRS Posi = new PRS(CenterPosi.position, Quaternion.Euler(0, -180, 0), Vector3.one * 45f);

        var target = CardObject[CurrentCount];
        target.originPosi = Posi;
        target.MoveTransForm(target.originPosi, true, 0.7f);

    }



    IEnumerator CardDrew()//가챠겟팩 값에의한 카드정보입력
    {
        Action = true;




        for (int i = 0; i < Count; i++)//카드이미지입력
        {
            CardObject[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = CardDataBase.cards[Data.GetPack[i]].BG_Image;
            Image Temp_Image = CardObject[i].transform.GetChild(1).GetChild(1).GetComponent<Image>();
            Temp_Image.sprite = CardDataBase.cards[Data.GetPack[i]].Image;
            Temp_Image.material = materials[CardDataBase.cards[Data.GetPack[i]].Rank - 1];

            if (CardDataBase.cards[Data.GetPack[i]].Rank >= 3)
            {
                CardObject[i].Reset_Particle();
            }
            else
            {
                CardObject[i].Stop_Particle();
            }



            CardObject[i].transform.GetChild(1).GetChild(7).GetComponent<Image>().enabled = false;
            CardObject[i].transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = CardDataBase.cards[Data.GetPack[i]].cost + "";
            CardObject[i].transform.GetChild(1).GetChild(5).GetComponent<TextMeshProUGUI>().text = CardDataBase.cards[Data.GetPack[i]].CardName + "";
            CardObject[i].transform.GetChild(1).GetChild(6).GetComponent<TextMeshProUGUI>().text = CardDataBase.cards[Data.GetPack[i]].exp + "";

        }





        if (Data.UsePack == 1)
        {
            StartCoroutine(CardNextToMove());
            yield return new WaitForSeconds(CardDelay);
        }
        else
        {
            for (int i = 0; i < Count; i++)//카드차곡차곡왼쪽으로 배치
            {
                CardAlignment(i);
                yield return new WaitForSeconds(CardDelay);
            }
        }

        //ParticleON_OFF(false);
        Action = false;


    }

    void CardAlignment(int num)//카드정렬위치로
    {
        PRS Posi = new PRS(CardMovePosi[num], Quaternion.Euler(0, -180, 0), Vector3.one * 15f);

        var target = CardObject[num];
        target.originPosi = Posi;
        target.MoveTransForm(target.originPosi, true, 0.7f);



    }



    public void Reset()//다시 뽑기
    {
        if (CheckButton == false)
        {

            if (Data.SpellCardPack >= Data.UsePack)
            {
                if (Data.UsePack != 1)
                {
                    OK_Button.SetActive(true);
                }
                End_Button.SetActive(false);
                CheckButton = true;


                CardPosiReset();


                for (int i = 0; i < Data.UsePack; i++)
                {

                    CardObject[i].gameObject.SetActive(true);
                    CardObject[i].BackCardPosi();
                    CardObject[i].Reset_Particle();
                }


                Data.GetPack.Clear();
                for (int i = 0; i < Data.UsePack; i++)
                {
                    int Gold;
                    int num = Random.Range(1, 100);
                    if (num >= 90)
                    {
                        Gold = 1000;
                        num = CardGet3[Random.Range(0, CardGet3.Count)];
                    }
                    else if (num >= 50)
                    {
                        Gold = 100;
                        num = CardGet2[Random.Range(0, CardGet2.Count)];
                    }
                    else
                    {
                        Gold = 10;
                        num = CardGet1[Random.Range(0, CardGet1.Count)];
                    }


                    int c = 0;
                    for (int j = 0; j < Data.DeckCards.Count; j++)
                    {
                        if (num == Data.DeckCards[j])
                        {
                            c++;
                        }
                    }
                    for (int j = 0; j < Data.HaveCard.Count; j++)
                    {
                        if (num == Data.HaveCard[j])
                        {
                            c++;
                        }
                    }

                    if (c < 3) //3장미만일경우
                    {
                        Data.HaveCard.Add(num);


                    }
                    else//3장이상인경우
                    {
                        Data.Gold += Gold;
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
                StartCoroutine(CardDrew());
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
