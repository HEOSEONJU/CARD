using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardGetUI : MonoBehaviour
{

    public Camera Maincam;
    public CardScriptable CardDataBase;
    public CharCardScriptTable CharDataBase;
    public Camera Subcam;
    public Camera SubcamChar;
    public GameObject Card;
    public GameObject Monster;
    public GameObject Result;
    public Transform LightPosi;
    public TextMeshProUGUI PackText;
    public Image FadeImage;
    public RandomScene RanFade;
    public RandomScene CharanFade;
    public FailWinodw POPUP_1;
    PlayerInfos Data;
    Vector3 a;
    Vector3 b;
    bool check;
    public int PackCount;
    //public TMP_InputField CostValue;
    [Header("얻은카드")]
    public List<int> GetCardNum;
    [Header("스펠카드등급별 나누기")]
    public List<int> CardGet1;
    public List<int> CardGet2;
    public List<int> CardGet3;


    [Header("캐릭터카드등급별 나누기")]
    public List<int> CharGet1;
    public List<int> CharGet2;
    public List<int> CharGet3;

    private void Awake()
    {
        Data = GetComponent<UIDataUpdate>().Data;



        check = true;

        CardGet1 = new List<int>();
        CardGet2 = new List<int>();
        CardGet3 = new List<int>();

        CharGet1 = new List<int>();
        CharGet2 = new List<int>();
        CharGet3 = new List<int>();

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


        a = new Vector3(-0.8888889f, -4.481482f, 90);
        b = new Vector3(-0.05740738f, -4.481482f, 90);
    }



    private void OnEnable()
    {

        PackCount = Data.SpellCardPack;
        PackText.text = "" + Data.SpellCardPack;
    }
    public void ResetPackText()
    {
        PackText.text = "" + Data.SpellCardPack;
    }






    public void UseCardPack(int n)
    {
        if (check == true)//캐릭터뽑기
        {
            if (Data.SpellCardPack >= n)
            {

                GetCardNum = new List<int>();
                Data.GetPack.Clear();
                for (int i = 0; i < n; i++)
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


                    //중복체크후 없다면
                    bool ck = true;
                    for (int j = 0; j < Data.MonsterCards.Count; j++)
                    {
                        if (num == Data.MonsterCards[j].ID)
                        {
                            //중복탐지했음

                            if (Data.MonsterCards[j].BreaK_Lim <= 4)
                            {
                                Data.MonsterCards[j].BreaK_Lim++;

                            }
                            else//한계돌파가끝났을경우
                            {
                                GetComponent<UIDataUpdate>().Data.Gold += Gold;
                            }

                            ck = false;
                            break;
                        }


                    }

                    if (ck == true)//중복이없으므로 새로얻은카드
                    {
                        MonsterInfo temp = new MonsterInfo();
                        temp.Init_Monset(num);
                        Data.MonsterCards.Add(temp);

                    }
                    Data.GetPack.Add(num);
                    GetCardNum.Add(num);
                }

                PackCount -= n;
                PackText.text = "" + PackCount;
                Data.SpellCardPack = PackCount;
                Data.UsePack = n;
                StartCoroutine(FadeScene(0.5f));
                Data.Saved_Data();
            }
            else
            {
                POPUP_1.View_Text("티켓이 모자랍니다");
            }
        }
        else//카드뽑기
        {
            if (Data.SpellCardPack >= n)
            {
                int Gold = 0;
                GetCardNum = new List<int>();
                Data.GetPack = new List<int>();
                for (int i = 0; i < n; i++)
                {
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
                    for (int j = 0; j<Data.DeckCards.Count; j++)
                    {
                        if(num==Data.DeckCards[j])
                        {
                            c++;
                        }
                    }
                    for (int j = 0; j < Data.HaveCard.Count ; j++)
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
                }

                PackCount -= n;
                PackText.text = "" + PackCount;
                Data.SpellCardPack = PackCount;
                Data.UsePack = n;
                StartCoroutine(FadeScene(0.5f));
                Data.Saved_Data();
            }
            else
            {
                POPUP_1.View_Text("티켓이 모자랍니다");
            }
        }

    }

    IEnumerator FadeScene(float time)
    {
        Color color = FadeImage.color;
        color.a = 0.0f;
        FadeImage.color = color;
        while (color.a <= 1.0)
        {
            color = FadeImage.color;
            color.a += Time.deltaTime * 2;
            FadeImage.color = color;
            yield return new WaitForSeconds(Time.deltaTime / 2);
        }


        Maincam.gameObject.SetActive(false);
        //Maincam.enabled = false;
        //Maincam.transform.GetComponent<AudioListener>().enabled = false;
        if (check == true)
        {
            SubcamChar.gameObject.SetActive(true);
            CharanFade.OnEnableScene();
        }
        else
        {
            Subcam.gameObject.SetActive(true);
            RanFade.OnEnableScene();
        }
        //Subcam.enabled = true;
        //Subcam.transform.GetComponent<AudioListener>().enabled = true;

    }
    public void BackScene()
    {
        StartCoroutine(Fadeout(0.5f));
    }
    IEnumerator Fadeout(float time)
    {
        Color color = FadeImage.color;
        color.a = 1.0f;
        FadeImage.color = color;
        while (color.a > 0)
        {
            color = FadeImage.color;
            color.a -= Time.deltaTime * 2;
            FadeImage.color = color;
            yield return new WaitForSeconds(Time.deltaTime / 2);
        }


    }
    public void SwapGetItem()
    {
        check = !check;
        if (check)
        {
            Card.SetActive(false);
            Monster.SetActive(true);
            LightPosi.position = a;
        }
        else
        {
            Card.SetActive(true);
            Monster.SetActive(false);
            LightPosi.position = b;
        }

    }
}
